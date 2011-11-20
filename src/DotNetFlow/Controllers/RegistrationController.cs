using System.Web.Mvc;
using System.Web.Security;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.Infrastructure.Eventing;
using DotNetFlow.Core.Services;

namespace DotNetFlow.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IUniqueIdentifierGenerator _idGenerator;
        private readonly IHashPasswords _passwordHashing;

        public RegistrationController(ICommandService commandService, IUniqueIdentifierGenerator idGenerator, IHashPasswords passwordHashing)
        {
            _commandService = commandService;
            _idGenerator = idGenerator;
            _passwordHashing = passwordHashing;
        }

        //
        // GET: /register

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /register

        [HttpPost]
        public ActionResult Create(RegisterUserAccountCommand command)
        {
            if (ModelState.IsValid)
            {
                command.UserId = _idGenerator.GenerateNewId();
                command.Password = _passwordHashing.HashPassword(command.Password);

                _commandService.Execute(command);

                LoginUser(command.Username);

                return RedirectToRoute("Home");
            }

            return View();
        }

        private static void LoginUser(string username)
        {
            FormsAuthentication.SetAuthCookie(username.Trim(), true);
        }
    }
}