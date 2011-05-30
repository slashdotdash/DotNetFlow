using System.Configuration;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Eventing.Sourcing;
using NUnit.Framework;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class EventDenormalizerTestFixture<TEvent> : EventTestFixture<TEvent> where TEvent : ISourcedEvent
    {
        protected IUnitOfWork UnitOfWork;        

        protected override void SetupDependencies()
        {
            UnitOfWork = new UnitOfWork(ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString);
            UnitOfWork.Initialize();
        }        

        [TestFixtureSetUp]
        public void SetUp()
        {
            new DatabaseMigrator().MigrateToLastVersion();            
        }

        [TearDown]
        public void Dispose()
        {
            if (UnitOfWork == null) return;

            UnitOfWork.Rollback();
            UnitOfWork.Dispose();
        }
    }
}