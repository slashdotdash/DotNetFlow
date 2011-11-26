using System;
using System.Collections.Generic;
using System.Linq;
using EventStore;
using EventStore.Dispatcher;

namespace DotNetFlow.Core.Infrastructure.Eventing
{
    public sealed class EventDispatcher : IDispatchCommits
    {
        private readonly Dictionary<Type, ICollection<Action<IDomainEvent>>> _handlers = new Dictionary<Type, ICollection<Action<IDomainEvent>>>();
        
        public void Dispatch(Commit commit)
        {
            foreach (var @event in commit.Events)
            {
                Dispatch((IDomainEvent)@event.Body);
            }
        }

        /// <summary>
        /// Dispatc the given event to all registered event handlers
        /// </summary>
        /// <param name="event"></param>
        private void Dispatch(IDomainEvent @event)
        {
            var typeOfEvent = @event.GetType();
            var handlers = GetEventHandlersForEvent(typeOfEvent).ToList();

            handlers.ForEach(handler => handler(@event));
        }

        public void RegisterHandler<TEvent>(Type typeofEvent, Action<TEvent> handler) where TEvent : IDomainEvent
        {
            if (!_handlers.ContainsKey(typeofEvent))
                _handlers[typeofEvent] = new List<Action<IDomainEvent>>();

            Action<IDomainEvent> action = evnt => handler((TEvent)evnt);

            _handlers[typeofEvent].Add(action);            
        }

        private IEnumerable<Action<IDomainEvent>> GetEventHandlersForEvent(Type typeOfEvent)
        {
            ICollection<Action<IDomainEvent>> result;
            _handlers.TryGetValue(typeOfEvent, out result);
            return result;
        }

        public void Dispose()
        {
        }
    }
}