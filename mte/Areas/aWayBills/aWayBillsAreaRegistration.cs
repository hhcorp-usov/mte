using System.Web.Mvc;

namespace mte.Areas.aWayBills
{
    public class aWayBillsAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "aWayBills";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "aWayBills_default",
                "aWayBills/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}