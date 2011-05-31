using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;
using Ncqrs.Commanding.ServiceModel;

namespace DotNetFlow.Areas.Admin.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IRepository<Submission> _repository;

        public SubmissionsController(ICommandService commandService, IRepository<Submission> repository)
        {
            _commandService = commandService;            
            _repository = repository;
        }

        //
        // GET: /admin/submissions

        public ActionResult Index()
        {
            var submissions = _repository.All().OrderBy(s => s.SubmittedAt);
            return View(submissions);
        }

        //
        // GET: /Admin/Publishing/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Publishing/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Admin/Publishing/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /Admin/Publishing/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Publishing/Edit/5

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
        // GET: /Admin/Publishing/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Publishing/Delete/5

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
