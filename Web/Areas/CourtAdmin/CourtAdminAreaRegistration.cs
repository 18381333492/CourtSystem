using System.Web.Mvc;

namespace Web.Areas.CourtAdmin
{
    public class CourtAdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "CourtAdmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "CourtAdmin_default",
                "CourtAdmin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
