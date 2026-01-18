using System.Web.Mvc;

namespace GrantApp.Areas.VDCMUNLevel
{
    public class VDCMUNLevelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "VDCMUNLevel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "VDCMUNLevel_default",
                "VDCMUNLevel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}