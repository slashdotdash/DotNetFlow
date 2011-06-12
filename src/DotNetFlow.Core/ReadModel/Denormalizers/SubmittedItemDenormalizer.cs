using Dapper;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Core.ReadModel.Denormalizers
{
    public sealed class SubmittedItemDenormalizer : IEventHandler<NewItemSubmittedEvent>, IEventHandler<ItemPublishedEvent>
    {
        private readonly IUnitOfWork _context;

        public SubmittedItemDenormalizer(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork;
        }

        public void Handle(NewItemSubmittedEvent evnt)
        {
            _context.Connection.Execute(
                "insert into Submissions (ItemId, SubmittedAt, UsersName, Title, HtmlContent) values (@ItemId, @SubmittedAt, @SubmissionUsersName, @Title, @HtmlContent)",
                new { evnt.ItemId, evnt.SubmittedAt, evnt.SubmissionUsersName, evnt.Title, evnt.HtmlContent },
                _context.Transaction);
        }

        /// <summary>
        /// Delete the submission details on approve/publish
        /// </summary>
        public void Handle(ItemPublishedEvent evnt)
        {
            _context.Connection.Execute("delete from Submissions where ItemId = @ItemId", new { evnt.ItemId }, _context.Transaction);
        }
    }
}