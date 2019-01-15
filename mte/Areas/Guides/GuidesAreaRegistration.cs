using System.Web.Mvc;

namespace mte.Areas.Guides
{
    public class GuidesAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Guides";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Guides_default",
                "Guides/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "mte.Areas.Guides.Controllers" }
            );
        }
    }
}