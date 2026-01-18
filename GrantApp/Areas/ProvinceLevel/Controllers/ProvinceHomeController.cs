using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;




namespace GrantApp.Areas.ProvinceLevel.Controllers
{
    [Authorize]
    public class ProvinceHomeController : Controller
    {
        // GET: ProvinceLevel/ProvinceHome
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            int CurrentLoginUseriD = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserClientId();
            return View();
        }
    }
}