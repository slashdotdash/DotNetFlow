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
        protected IStoreEvents EventStore { get; private set; }
        protected IRepository Context { get; private set; }

        protected Commit ActualCommit { get; private set; }

        protected IList<EventMessage> CommittedEvents
        {
            get { return ActualCommit.Events; }
        }

        protected TCommand ExecutedCommand { get; private set; }

        protected abstract TCommand WhenExecuting();

        protected abstract void Execute(TCommand command);

        protected virtual void SetupDependencies()
        {
            EventStore = ConfigureEventStore();
            Context = new EventStoreRepository(EventStore, new SimpleAggregateCreationStrategy(), new ConflictDetector());

            ObjectFactory.Initialize(config => config.For<IRepository>().Use(Context));
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

        public override void Given()
        {
            SetupDependencies();
            ExecutedCommand = WhenExecuting();
        }

        public override void When()
        {
            Execute(ExecutedCommand);
        }

        public override void Finally()
        {
            ObjectFactory.EjectAllInstancesOf<IRepository>();
        }
    }
}