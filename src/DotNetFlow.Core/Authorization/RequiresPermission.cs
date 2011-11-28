using System.Net;
using System.Web.Mvc;

namespace DotNetFlow.Core.Authorization
{
    public sealed class RequiresPermission : AuthorizeAttribute
    {
        /// <summary>
        /// Redirect to the login page for unauthorized requests
        /// </summary>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // Returns HTTP 403 Forbidden error for loged in users without the requested permission
                filterContext.Result = new HttpStatusCodeResult((int)HttpStatusCode.Forbidden);               
            }
            else
            {
                // Redirect to login page for anonymous users
                filterContext.Result = new HttpUnauthorizedResult();
                //filterContext.Result = new RedirectToRouteResult("Login", null, false);                
            }
        }
    }
}