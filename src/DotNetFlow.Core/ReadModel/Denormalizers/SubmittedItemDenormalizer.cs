using System.Data;
using Dapper;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Core.ReadModel.Denormalizers
{
    public sealed class SubmittedItemDenormalizer : IEventHandler<NewItemSubmittedEvent>
    {
        private readonly IDbConnection _connection;
        private readonly IDbTransaction _transaction;

        public SubmittedItemDenormalizer(IUnitOfWork unitOfWork)
        {
            _connection = unitOfWork.Connection;
            _transaction = unitOfWork.Transaction;
        }

        //public SubmittedItemDenormalizer(IDbConnection connection, IDbTransaction transaction = null)
        //{
        //    _connection = connection;
        //    _transaction = transaction;
        //}

        public void Handle(NewItemSubmittedEvent evnt)
        {
            _connection.Execute("insert into Submissions (ItemId, Title) values (@ItemId, @Title)", new { evnt.ItemId, evnt.Title }, _transaction); 
        }
    }
}