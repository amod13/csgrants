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

    public class AdminFileUploadsController : Controller
    {
        ReportServices services = new ReportServices();
        public ActionResult Index()
        {
            ReportModel model = new ReportModel();
            model.ProvinceIdSearch = 1;
            model.DistrictIdSearch = 0;
            model.ProgramPhaseNumber = CommontUtilities.GetCurrentProgramPhaseNumber();
            return View(model);
        }

        [HttpPost]
        public ActionResult AyojanaList(ReportModel model)
        {

            model.AdhuroApuroGrantRequestListVM = new List<AdhuroApuroReportViewModel>();
            model.AdhuroApuroGrantRequestListVM = services.AdhuroApuroAppliedListReportForAdmin(model);

            return PartialView("_AdhuroApuroList", model);

        }

    }
}