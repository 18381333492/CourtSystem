using System.Web.Mvc;

namespace Web.Areas.Pays
{
    public class PaysAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Pays";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Pays_default",
                "Pays/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
