using System.Web.Mvc;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Extensions;

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
        // POST: /admin/publish

        [HttpPost]
        public ActionResult Create(PublishItemCommand command)
        {
            //command.ApprovedByUserId = TODO

            _commandService.Execute(command);

            return RedirectToRoute("PendingApproval")
                .Success("Submission approved");
        }
    }
}