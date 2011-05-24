using System.Data;
using Dapper;
using Ncqrs.Eventing.ServiceModel.Bus;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Core.ReadModel.Denormalizers
{
    public sealed class SubmittedItemDenormalizer : IEventHandler<NewItemSubmittedEvent>
    {
        private readonly IDbConnection _connection;

        public SubmittedItemDenormalizer(IDbConnection connection)
        {
            _connection = connection;
        }

        public void Handle(NewItemSubmittedEvent evnt)
        {
            _connection.Execute("insert into Submissions (ItemId, Title) values (@ItemId, @Title)", new { evnt.ItemId, evnt.Title }); 
        }
    }
}