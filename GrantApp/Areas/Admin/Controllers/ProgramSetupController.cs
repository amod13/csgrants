using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Models;
using GrantApp.Services;

namespace GrantApp.Areas.Admin.Controllers
{
    [Authorize]
    public class ProgramSetupController : Controller
    {
        ProgramServices services = new ProgramServices();
        // GET: Admin/ProgramSetup

        #region Grant Type

        public ActionResult ListGrantType()
        {
            GrantTypeViewModel model = new GrantTypeViewModel();
            model.GrantTypeViewModelList = services.PopulateGrantType();
            return View(model);
        }

        #endregion


        public ActionResult Index(int id)
        {
            ProgramSetup model = new ProgramSetup();

            model.ProgramSetupList = services.PopulateProgram(id);//Grant Type Id wise
            model.ViewBagGrantTypeId = id;
            return View(model);
        }

        public ActionResult Create(int id)
        {
            ProgramSetup model = new ProgramSetup();
            model.ViewBagGrantTypeId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProgramSetup model)
        {
            model.GrantTypeId = model.ViewBagGrantTypeId;
            services.InsertProgram(model);
            return RedirectToAction("Index", new { @id = model.GrantTypeId });
        }

        public ActionResult Edit(int id, int id1)
        {
            ProgramSetup model = new ProgramSetup();
            model = services.PopulateProgram(id1).SingleOrDefault(x => x.ProgramId == id);
            model.ViewBagGrantTypeId = id1;
            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(ProgramSetup model)
        {
            model.GrantTypeId = model.ViewBagGrantTypeId;
            services.UpdateProgram(model);
            return RedirectToAction("Index", new { @id = model.ViewBagGrantTypeId });
        }



    }
}