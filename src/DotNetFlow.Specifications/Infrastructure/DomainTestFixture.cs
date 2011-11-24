using System;
using System.Collections.Generic;
using CommonDomain.Core;
using CommonDomain.Persistence;
using CommonDomain.Persistence.EventStore;
using DotNetFlow.Core.Infrastructure.Aggregates;
using DotNetFlow.Core.Infrastructure.Commanding;
using EventStore;
using EventStore.Dispatcher;
using StructureMap;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class DomainTestFixture<TCommand> : SpecificationBase
        where TCommand : ICommand
    {
        public override void Given()
        {
            SetupDependencies();
            ExecutedCommand = WhenExecuting();
        }

        public override void When()
        {
            try
            {
                Execute(ExecutedCommand);
            }
            catch (Exception ex)
            {
                CaughtException = ex;
            }
        }

        public override void Finally()
        {
            ObjectFactory.EjectAllInstancesOf<IStoreEvents>();
            ObjectFactory.EjectAllInstancesOf<IRepository>();
        }

        protected abstract TCommand WhenExecuting();
        protected abstract void Execute(TCommand command);

        protected TCommand ExecutedCommand { get; private set; }
        protected IStoreEvents EventStore { get; set; }
        protected IRepository Repository { get; set; }
        protected Commit ActualCommit { get; private set; }
        protected Exception CaughtException { get; private set; }

        protected IList<EventMessage> CommittedEvents
        {
            get { return ActualCommit.Events; }
        }
        
        protected virtual void SetupDependencies()
        {
            EventStore = ConfigureEventStore();
            Repository = new EventStoreRepository(EventStore, new SimpleAggregateCreationStrategy(), new ConflictDetector());

            ObjectFactory.Configure(ConfigureStructureMap);
        }
        
        protected virtual void ConfigureStructureMap(ConfigurationExpression config)
        {
            config.For<IStoreEvents>().Use(EventStore);
            config.For<IRepository>().Use(Repository);
        }

        private IStoreEvents ConfigureEventStore()
        {
            return Wireup.Init()
                .UsingInMemoryPersistence()
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .UsingSynchronousDispatchScheduler().DispatchTo(CreateDispatcher())
                .Build();
        }

        private IDispatchCommits CreateDispatcher()
        {
            return new DelegateMessageDispatcher(Dispatch);
        }

        private void Dispatch(Commit commit)
        {
            ActualCommit = commit;            
        }        
    }
}