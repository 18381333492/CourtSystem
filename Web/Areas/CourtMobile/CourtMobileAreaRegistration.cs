using System.Web.Mvc;

namespace Web.Areas.CourtMobile
{
    public class CourtMobileAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CourtMobile";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CourtMobile_default",
                "CourtMobile/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
