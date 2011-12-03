using System;
using System.Web.Mvc;
using DotNetFlow.Core.Authorization;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;
using DotNetFlow.Core.Services;
using DotNetFlow.Extensions;

namespace DotNetFlow.Areas.Admin.Controllers
{
    [RequiresPermission(Users = "ben,approver")]
    public class PublishingController : Controller
    {
        private readonly ICommandService _commandService;
        private readonly IGenerateUrlSlug _urlSlugGenerator;
        private readonly IReadModelRepository<Submission> _readModelRepository;

        public PublishingController(ICommandService commandService, IGenerateUrlSlug urlSlugGenerator, IReadModelRepository<Submission> readModelRepository)
        {
            _commandService = commandService;
            _urlSlugGenerator = urlSlugGenerator;
            _readModelRepository = readModelRepository;
        }

        //
        // POST: /admin/publish

        [HttpPost]
        public ActionResult Create(PublishItemCommand command)
        {
            //command.ApprovedBy = TODO
            command.UrlSlug = CreateUrlSlugForItem(command.ItemId);
            command.PublishedAt = DateTime.UtcNow;

            _commandService.Execute(command);

            return RedirectToRoute("PendingApproval")
                .Success("Submission approved");
        }

        private string CreateUrlSlugForItem(Guid itemId)
        {
            var item = _readModelRepository.Get(itemId);

           return _urlSlugGenerator.Slugify(item.Title);
        }
    }
}