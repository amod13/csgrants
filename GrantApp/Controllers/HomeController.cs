using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GrantApp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        //User Mannual File Download
        public ActionResult DownloadMannual()
        {
            return File("~/Content/RASTRIYAYOJANAAAYOG.pdf", "application/pdf");
        }

        public ActionResult ViewKaryabidhi()
        {
            return File("~/Content/csgrant.pdf", "application/pdf");
        }

        public ActionResult ViewKaryabidhibisesh2082()
        {
            return File("~/Content/csgrantkaryebidhi_bisesh2082.pdf", "application/pdf");
        }

        public ActionResult ViewKaryabidhisamapurak2082()
        {
            return File("~/Content/csgrantkaryebidhi_samapurak2082.pdf", "application/pdf");
        }

        public ActionResult ViewNoticeNew()
        {
            return File("~/Content/ViewNoticeNew.pdf", "application/pdf");
        }

        

        public ActionResult DownloadKaryaBidhiOld()
        {
            return File("~/Content/Notice.zip", "application/zip");
        }
        public ActionResult DownloadKaryaBidhi()
        {
            return File("~/Content/ViewUpdatedKaryabidhiNew.pdf", "application/pdf");
        }











    }
}