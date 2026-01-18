using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Models;

namespace GrantApp.Areas.Admin.Controllers
{
    [Authorize]
    public class UserRegistrationController : Controller
    {
        CommonProvider _Services = new CommonProvider();
        // GET: Admin/UserRegistration
        public ActionResult Index()
        {
            UsersModel _Model = new UsersModel();
            
            _Model.UserLoginModelListList = _Services.PopulateRegisteredUserList();
            return View(_Model);
        }

        public ActionResult ListAgencies()
        {
            AgencyDetailViewModel model = new AgencyDetailViewModel();
            model.AgencyDetailViewModelList = new List<AgencyDetailViewModel>();
            model.AgencyDetailViewModelList = _Services.SP_PopulateAgenciesList();
            return View(model);
        }
        public ActionResult CreateAgency()
        {
            AgencyDetailViewModel model = new AgencyDetailViewModel();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateAgency(AgencyDetailViewModel model)
        {
            if(_Services.SP_InsertAgencyDetail(model)>0)
            {
                TempData["SuccessNotification"] = "Agency Created Successfully";
                return RedirectToAction("ListAgencies");
            }
            else
            {
                ViewBag.ErrorMessage = "Error..Please try again";
                return View(model);
            }
           
        }


        public ActionResult EditAgency(int id)
        {
            AgencyDetailViewModel model = new AgencyDetailViewModel();
            model = _Services.SP_PopulateAgenciesList().SingleOrDefault(x => x.AgencyId == id);
            return View(model);
        }
        [HttpPost]
        public ActionResult EditAgency(AgencyDetailViewModel model)
        {
            if (_Services.SP_UpdateAgencyDetail(model)=="Updated Successfully")
            {
                TempData["SuccessNotification"] = "Agency Data Updated Successfully";
                return RedirectToAction("ListAgencies");
            }
            else
            {
                ViewBag.ErrorMessage = "Error..Please try again";
                return View(model);
            }

        }
        public ActionResult DeleteAgency(int id)
        {
            if(_Services.SP_DeleteAgencyDetail(id)=="Deleted Successfully")
            {
                TempData["SuccessNotification"] = "Agency data deleted Successfully";
            }
            else
            {
                TempData["SuccessNotification"] = "Error ... Please try again";
            }
            
            return RedirectToAction("ListAgencies");
        }



    }
}