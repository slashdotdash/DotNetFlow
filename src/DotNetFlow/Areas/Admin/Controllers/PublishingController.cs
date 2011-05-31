using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;
using Ncqrs.Commanding.ServiceModel;

namespace DotNetFlow.Areas.Admin.Controllers
{
    public class PublishingController : Controller
    {
        private readonly ICommandService _commandService;

        public PublishingController(ICommandService commandService)
        {
            _commandService = commandService;            
        }

        //
        // GET: /admin/publish

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /admin/publish

        [HttpPost]
        public ActionResult Create(PublishItemCommand collection)
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
    }
}
