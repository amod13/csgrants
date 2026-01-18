using System.Web.Mvc;

namespace GrantApp.Areas.ProvinceLevel
{
    public class ProvinceLevelAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ProvinceLevel";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ProvinceLevel_default",
                "ProvinceLevel/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}