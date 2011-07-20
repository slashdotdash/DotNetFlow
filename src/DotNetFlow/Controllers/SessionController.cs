using System.Web.Mvc;
using System.Web.Security;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.Services;
using DotNetFlow.Extensions;

namespace DotNetFlow.Controllers
{
    /// <summary>
    /// Handles log-in and log-out functionality
    /// </summary>
    public class SessionController : Controller
    {
        private readonly IAuthenticationService _authentication;

        public SessionController(IAuthenticationService authentication)
        {
            _authentication = authentication;
        }

        //
        // GET: /login

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /login

        /// <summary>
        /// Create a new session (login user when correct credentials entered)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(LoginUserCommand command)
        {
            if (ModelState.IsValid)
            {
                var authorisation = _authentication.Authenticate(command.UsernameOrEmail, command.Password);
                if (authorisation.IsAuthenticated)
                {
                    LoginUser(authorisation.User);
                    return RedirectToRoute("Home");   
                }

                ModelState.AddModelError("*", "Login failed, please check your username or e-mail address and password and try again.");
            }

            return View();
        }

        private static void LoginUser(UserAccountModel user)
        {
            FormsAuthentication.SetAuthCookie(user.Username, true);
        }

        //
        // GET: /logout
 
        public ActionResult Delete()
        {
            return View();
        }

        //
        // POST: /logout

        [HttpPost]
        public ActionResult Delete(FormCollection collection)
        {
            FormsAuthentication.SignOut();
            return Redirect(Url.Home());
        }
    }
}
