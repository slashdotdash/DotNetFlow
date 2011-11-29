using System.Web.Mvc;
using System.Web.Routing;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Infrastructure;
using FluentValidation.Mvc;
using StructureMap;

namespace DotNetFlow
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new UseUnitOfWork(ObjectFactory.GetInstance<IUnitOfWork>));
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Ignore favicon.ico
            routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });

            routes.MapRoute("Login", "login", new { controller = "Session", action = "Create" });
            routes.MapRoute("Logout", "logout", new { controller = "Session", action = "Delete" });
            routes.MapRoute("Register", "register", new { controller = "Registration", action = "Create" });
            routes.MapRoute("SubmitItem", "submit", new { controller = "Submissions", action = "Create" });
            routes.MapRoute("YourSubmission", "your-submission/{id}", new { controller = "Submissions", action = "Show" });
            routes.MapRoute("PublishedItem", "items/{slug}", new { controller = "PublishedItems", action = "Show" });
            routes.MapRoute("Home", string.Empty, new { controller = "PublishedItems", action = "Index" });

            // Map default root
            routes.MapRoute("Default", "{controller}/{action}/{id}", new { controller = "Home", action = "Index", id = UrlParameter.Optional });
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
            ObjectFactory.ReleaseAndDisposeAllHttpScopedObjects();
        }

        private static void EnableFluentValidation()
        {
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new StructureMapValidatorFactory())); 
        }
    }
}