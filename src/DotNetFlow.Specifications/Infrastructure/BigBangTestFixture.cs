using System;
using System.Collections.Generic;
using System.Linq;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Core.Infrastructure.Eventing;
using EventStore;
using StructureMap;

namespace DotNetFlow.Specifications.Infrastructure
{
    public abstract class BigBangTestFixture<TCommand> : DomainTestFixture<TCommand> where TCommand : ICommand
    {
        protected Guid EventSourceId { get; private set; }
        protected int StreamRevision { get; set; }

        protected IEnumerable<EventMessage> PublishedEvents
        {
            get { return EventStore.OpenStream(EventSourceId, StreamRevision, int.MaxValue).CommittedEvents; }
        }

        protected virtual IEnumerable<IDomainEvent> GivenEvents()
        {
            return new IDomainEvent[0];
        }

        protected override void SetupDependencies()
        {
            ObjectFactory.Initialize(config => config.AddRegistry(new DomainRegistry()));
            base.SetupDependencies();

            EventStore = ObjectFactory.GetInstance<IStoreEvents>();

            GenerateEventSourceId();
            RecordGivenEvents();        
        }

        protected override void Execute(TCommand command)
        {
            var commandService = ObjectFactory.GetInstance<ICommandService>();
            commandService.Execute(command);
        }

        private void GenerateEventSourceId()
        {
            var idGenerator = ObjectFactory.GetInstance<IUniqueIdentifierGenerator>();
            EventSourceId = idGenerator.GenerateNewId();
        }

        private void RecordGivenEvents()
        {
            var events = GivenEvents();

            using (var stream = PrepareStream(events))
            {
                stream.CommitChanges(Guid.NewGuid());
                StreamRevision = stream.StreamRevision + 1;
            }
        }

        private IEventStream PrepareStream(IEnumerable<IDomainEvent> events)
        {
            var stream = EventStore.CreateStream(EventSourceId);

            events.Select(x => new EventMessage { Body = x })
                .ToList()
                .ForEach(stream.Add);

            return stream;
        }
    }
}