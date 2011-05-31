using System.Web.Mvc;

namespace DotNetFlow.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("PendingApproval", "admin/pending", new { controller = "PendingApproval", action = "Index" });
            context.MapRoute("Publish", "admin/publish", new { controller = "Publishing", action = "Index" });

            context.MapRoute(
                "Admin_default",
                "Admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
