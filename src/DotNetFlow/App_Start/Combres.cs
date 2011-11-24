[assembly: WebActivator.PreApplicationStartMethod(typeof(DotNetFlow.App_Start.Combres), "PreStart")]
namespace DotNetFlow.App_Start {
	using System.Web.Routing;
	using global::Combres;
	
    public static class Combres {
        public static void PreStart() {
            RouteTable.Routes.AddCombresRoute("Combres");
        }
    }
}