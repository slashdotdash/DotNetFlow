using System;
using System.Collections.Generic;
using System.Linq;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Eventing.Storage;

namespace DotNetFlow.Specifications.Infrastructure
{
    internal sealed class InternalEventStore : IEventStore
    {
        private readonly List<ISourcedEvent> _events;

        public InternalEventStore(List<ISourcedEvent> events)
        {
            _events = events;
        }

        public InternalEventStore(IEnumerable<ISourcedEvent> events)
        {
            _events = events.ToList();
        }

        public InternalEventStore()
            : this(new List<ISourcedEvent>())
        {
        }

        public IEnumerable<ISourcedEvent> GetAllEvents(Guid id)
        {
            return _events;
        }

        public IEnumerable<ISourcedEvent> GetAllEventsSinceVersion(Guid id, long version)
        {
            return _events.Where(e => e.EventSequence >= version);
        }

        public void Save(IEventSource source)
        {
            _events.AddRange(source.GetUncommittedEvents());
        }
    }
}