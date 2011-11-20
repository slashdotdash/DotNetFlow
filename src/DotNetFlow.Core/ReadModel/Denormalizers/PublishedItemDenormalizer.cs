using Dapper;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Core.ReadModel.Denormalizers
{
    public sealed class PublishedItemDenormalizer : IEventHandler<ItemPublishedEvent>
    {
        private readonly IUnitOfWork _context;

        public PublishedItemDenormalizer(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork;
        }

        public void Handle(ItemPublishedEvent evnt)
        {
            _context.Connection.Execute(
                "insert into Items (ItemId, PublishedAt, SubmittedByUser, Title, HtmlContent) values (@ItemId, @PublishedAt, @SubmittedByUser, @Title, @HtmlContent)",
                new { evnt.ItemId, evnt.PublishedAt, evnt.SubmittedByUser, evnt.Title, evnt.HtmlContent },
                _context.Transaction);
        }
    }
}