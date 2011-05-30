using System.Web.Mvc;

namespace DotNetFlow.Extensions
{
    /// <summary>
    /// Convenience extension methods for generating common routing URLs
    /// </summary>
    public static class UrlHelperExtensions
    {
        public static string Home(this UrlHelper helper)
        {
            return helper.Content("~/");
        }

        public static string Login(this UrlHelper helper)
        {
            return helper.RouteUrl("Login");
        }

        public static string Logout(this UrlHelper helper)
        {
            return helper.RouteUrl("Logout");
        }

        public static string SignUp(this UrlHelper helper)
        {
            return helper.RouteUrl("Register");
        }
        
        public static string SubmitItem(this UrlHelper helper)
        {
            return helper.RouteUrl("SubmitItem");
        }
    }
}