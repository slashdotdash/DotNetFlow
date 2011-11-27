using Dapper;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.Infrastructure.Eventing;

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
                "insert into Submissions (ItemId, SubmittedAt, UserId, Username, FullName, Title, HtmlContent) values (@ItemId, @SubmittedAt, @UserId, @Username, @FullName, @Title, @HtmlContent)",
                new { evnt.ItemId, evnt.SubmittedAt, evnt.UserId, evnt.Username, evnt.FullName, evnt.Title, evnt.HtmlContent },
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