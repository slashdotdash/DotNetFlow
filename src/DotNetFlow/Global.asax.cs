using System.Web.Mvc;
using System.Web.Routing;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Infrastructure;
using FluentValidation.Mvc;

namespace DotNetFlow
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UseUnitOfWork());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute("Login", "login", new { controller = "Session", action = "Create" });
            routes.MapRoute("Logout", "logout", new { controller = "Session", action = "Delete" });
            routes.MapRoute("Register", "register", new { controller = "Registration", action = "Create" });
            routes.MapRoute("SubmitItem", "submit", new { controller = "Submissions", action = "Create" });
            routes.MapRoute("Home", "", new { controller = "Items", action = "Index" });
            
            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Bootstrapper.Configure();
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());           

            EnableFluentValidation();
        }
        
        protected void Application_EndRequest()
        {            
            //ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        private static void EnableFluentValidation()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new StructureMapValidatorFactory())); 
        }
    }
}