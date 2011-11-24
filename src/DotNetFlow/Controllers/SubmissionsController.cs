using System;
using System.Web.Mvc;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.Infrastructure.Eventing;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;

namespace DotNetFlow.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IUniqueIdentifierGenerator _idGenerator;
        private readonly IReadModelRepository<Submission> _readModelRepository;

        public SubmissionsController(ICommandService commandService, IUniqueIdentifierGenerator idGenerator, IReadModelRepository<Submission> readModelRepository)
        {
            _commandService = commandService;
            _idGenerator = idGenerator;
            _readModelRepository = readModelRepository;
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
            var submission = _readModelRepository.Get(id);
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