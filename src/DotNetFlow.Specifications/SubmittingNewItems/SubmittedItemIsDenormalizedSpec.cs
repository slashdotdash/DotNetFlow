using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Spec;
using NUnit.Framework;
using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.ReadModel.Denormalizers;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace DotNetFlow.Specifications.SubmittingNewItems
{
    [Specification, Integration]
    public sealed class SubmittedItemIsDenormalizedSpec : EventTestFixture<NewItemSubmittedEvent>
    {
        private IUnitOfWork _unitOfWork;
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        protected override NewItemSubmittedEvent WhenExecutingEvent()
        {
            return new NewItemSubmittedBuilder().Build();
        }

        protected override IEventHandler<NewItemSubmittedEvent> BuildEventHandler()
        {
            return new SubmittedItemDenormalizer(_unitOfWork);
        }

        [Then]
        public void Should_Insert_SubmittedItem()
        {
            var submissions = _unitOfWork.Connection.Query<int>("select count(*) from Submissions", null, _transaction);
            Assert.AreEqual(1, submissions.Single());
        }

        protected override void SetupDependencies()
        {
            _unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString);
            _unitOfWork.Initialize();
            //_connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString);
            //_connection.Open();

            //_transaction = _connection.BeginTransaction();
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            if (_unitOfWork != null)
            {
                _unitOfWork.Rollback();
                _unitOfWork.Dispose();
            }
            //if (_transaction != null) 
            //    _transaction.Rollback();
            
            //if (_connection != null) 
            //    _connection.Close();
        }
    }
}