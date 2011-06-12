using Dapper;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;
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
                "insert into Items (ItemId, PublishedAt, UsersName, Title, HtmlContent) values (@ItemId, @PublishedAt, @SubmissionUsersName, @Title, @HtmlContent)",
                new { evnt.ItemId, evnt.PublishedAt, evnt.SubmissionUsersName, evnt.Title, evnt.HtmlContent },
                _context.Transaction);
        }
    }
}