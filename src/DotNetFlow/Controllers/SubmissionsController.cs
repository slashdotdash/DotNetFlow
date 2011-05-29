using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;

namespace DotNetFlow.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IUniqueIdentifierGenerator _idGenerator;
        private readonly IRepository<Submission> _repository;

        public SubmissionsController(ICommandService commandService, IUniqueIdentifierGenerator idGenerator, IRepository<Submission> repository)
        {
            _commandService = commandService;
            _idGenerator = idGenerator;
            _repository = repository;
        }

        //
        // GET: /Submissions/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Submissions/Details/5

        public ActionResult Details(Guid id)
        {
            var submission = _repository.Get(id);
            return View(submission);
        }

        //
        // GET: /Submissions/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Submissions/Create

        [HttpPost]
        public ActionResult Create(SubmitNewItemCommand command)
        {
            if (ModelState.IsValid)
            {
                command.ItemId = _idGenerator.GenerateNewId();
                _commandService.Execute(command);

                return RedirectToAction("Details", new { id = command.ItemId });
            }
            
            return View();            
        }
        
        //
        // GET: /Submissions/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Submissions/Edit/5

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
        // GET: /Submissions/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Submissions/Delete/5

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
