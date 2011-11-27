using System;
using System.Web.Mvc;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;

namespace DotNetFlow.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IUniqueIdentifierGenerator _idGenerator;
        private readonly IReadModelRepository<Submission> _submissionReadModelRepository;
        private readonly IUserReadModelRepository _userReadModelRepository;

        public SubmissionsController(ICommandService commandService, IUniqueIdentifierGenerator idGenerator, IReadModelRepository<Submission> submissionReadModelRepository, IUserReadModelRepository userReadModelRepository)
        {
            _commandService = commandService;
            _idGenerator = idGenerator;
            _submissionReadModelRepository = submissionReadModelRepository;
            _userReadModelRepository = userReadModelRepository;
        }
        
        //
        // GET: /your-submission

        public ActionResult Show(Guid id)
        {
            var submission = _submissionReadModelRepository.Get(id);
            return View(submission);
        }

        //
        // GET: /Submissions/Create

        public ActionResult Create()
        {
            var command = new SubmitNewItemCommand();
            IncludeAuthenticatedUserDetails(command);

            return View(command);
        }

        //
        // POST: /Submissions/Create

        [HttpPost]
        public ActionResult Create(SubmitNewItemCommand command)
        {
            IncludeAuthenticatedUserDetails(command);

            if (ModelState.IsValid)
            {
                GenerateSubmissionId(command);

                _commandService.Execute(command);

                return RedirectToRoute("YourSubmission", new { id = command.ItemId });
            }

            return View();
        }

        private void GenerateSubmissionId(SubmitNewItemCommand command)
        {
            command.ItemId = _idGenerator.GenerateNewId();            
        }

        private void IncludeAuthenticatedUserDetails(SubmitNewItemCommand command)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _userReadModelRepository.GetByUsername(User.Identity.Name);
                
                command.UserId = user.UserId;
                command.FullName = user.FullName;
                command.Username = user.Username;
            }
        }
    }
}