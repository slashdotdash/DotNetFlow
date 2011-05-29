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
            var submissions = _unitOfWork.Connection.Query<int>("select count(*) from Submissions", null, _unitOfWork.Transaction);
            Assert.AreEqual(1, submissions.Single());
        }

        protected override void SetupDependencies()
        {
            _unitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString);
            _unitOfWork.Initialize();            
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            if (_unitOfWork == null) return;

            _unitOfWork.Rollback();
            _unitOfWork.Dispose();
        }
    }
}