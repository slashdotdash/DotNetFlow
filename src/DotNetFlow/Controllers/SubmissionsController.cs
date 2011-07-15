using System;
using System.Web.Mvc;
using DotNetFlow.Core.Commands;
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
    }
}