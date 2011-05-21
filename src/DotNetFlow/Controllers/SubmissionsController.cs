using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetFlow.Core.Commands;
using Ncqrs.Commanding.ServiceModel;
using Ncqrs;

namespace DotNetFlow.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ICommandService _commandService;

        public SubmissionsController(ICommandService commandService)
        {
            _commandService = commandService;
        }

        //
        // GET: /Submissions/

        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Submissions/Details/5

        public ActionResult Details(int id)
        {
            return View();
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
            try
            {
                _commandService.Execute(command);
                
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
