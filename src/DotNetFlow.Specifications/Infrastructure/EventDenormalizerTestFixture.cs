using System.Configuration;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Eventing;
using NUnit.Framework;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class EventDenormalizerTestFixture<TEvent> : EventTestFixture<TEvent> where TEvent : IDomainEvent
    {
        protected IUnitOfWork UnitOfWork;

        protected override void SetupDependencies()
        {
            UnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString);
            UnitOfWork.Begin();
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            new DatabaseMigrator().MigrateToLastVersion();
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            if (UnitOfWork == null) return;

            UnitOfWork.Rollback();
            UnitOfWork.Dispose();
        }
    }
}