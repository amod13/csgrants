using DocumentFormat.OpenXml.EMMA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GrantApp.Areas.VDCMUNLevel.Controllers
{
    public class BaseController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            DateTime returnDate;
            int currentLoginUserType = GrantApp.Areas.Admin.FunctionClass.GetCurrentLoginUserType();

            if (currentLoginUserType == 2)
            {
                returnDate = GrantApp.StaticValue.ConstantValues.SubmissionDateForProvinceOnly;
            }
            else
            {
                returnDate = GrantApp.StaticValue.ConstantValues.SubmissionDateForProduction;
            }

            if (DateTime.Today > returnDate)
            {
                TempData["Notification"] = "DateExpired";
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Account", area="",action = "Logoff" })
                );
            }
        }

   
    }
}