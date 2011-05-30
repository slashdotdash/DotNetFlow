using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;

namespace DotNetFlow.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IUniqueIdentifierGenerator _idGenerator;
        private readonly IRepository<Submission> _repository;

        public RegistrationController(ICommandService commandService, IUniqueIdentifierGenerator idGenerator, IRepository<Submission> repository)
        {
            _commandService = commandService;
            _idGenerator = idGenerator;
            _repository = repository;
        }

        //
        // GET: /Registration/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Registration/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Registration/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Registration/Create

        [HttpPost]
        public ActionResult Create(RegisterUserAccountCommand command)
        {
            if (ModelState.IsValid)
            {
                command.UserId = _idGenerator.GenerateNewId();
                _commandService.Execute(command);

                // TODO: Log user in
                
                FormsAuthentication.SetAuthCookie(command.Email.Trim(), true);
                return RedirectToRoute("Home");
            }

            return View();
        }
        
        //
        // GET: /Registration/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Registration/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Registration/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Registration/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
