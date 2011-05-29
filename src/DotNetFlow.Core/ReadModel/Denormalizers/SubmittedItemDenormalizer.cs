using Dapper;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Core.ReadModel.Denormalizers
{
    public sealed class SubmittedItemDenormalizer : IEventHandler<NewItemSubmittedEvent>
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
    }
}