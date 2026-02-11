using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GrantApp.Services;
using GrantApp.Areas.Admin.Models;
using GrantApp.Models;
using System.IO;
using PagedList;
using PagedList.Mvc;
namespace GrantApp.Areas.Admin.Controllers
{
    [Authorize]
    public class InfographicsController : Controller
    {
        int CurrentPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();

        ReportServices services = new ReportServices();

        // GET: /Infographics/
        public ActionResult Index()
        {
            // Pass initial data to view
            ViewBag.CurrentPhase = CurrentPhaseNumber;
            

            return View();
        }

        [HttpPost]
        public JsonResult GetInsights(int phaseStatus, int grantTypeId = 0, int userType = 0)
        {
            InsightsDashboardVM model = new InsightsDashboardVM();

            model = services.GetInsights(phaseStatus, grantTypeId, userType);


            return Json(model);
        }

        public ActionResult Test()
        {
            return View();
        }



    }



}