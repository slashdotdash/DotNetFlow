using System;
using System.Collections.Generic;
using EventStore;
using EventStore.Dispatcher;

namespace DotNetFlow.Core.Infrastructure.Eventing
{
    public sealed class EventDispatcher : IDispatchCommits
    {
        private readonly Dictionary<Type, Action<IDomainEvent>> _handlers = new Dictionary<Type, Action<IDomainEvent>>();
        
        public void Dispatch(Commit commit)
        {
            foreach (var @event in commit.Events)
            {
                Dispatch((IDomainEvent)@event.Body);
            }
        }

        private void Dispatch(IDomainEvent @event)
        {
            var typeOfEvent = @event.GetType();
            var handler = GetEventHandlerForEvent(typeOfEvent);

            handler.Invoke(@event);
        }

        public void RegisterHandler(Type typeofEvent, Action<IDomainEvent> handler)
        {
            RegisterHandler<IDomainEvent>(typeofEvent, handler);
        }

        public void RegisterHandler<TEvent>(Type typeofEvent, Action<TEvent> handler) where TEvent : IDomainEvent
        {
            if (_handlers.ContainsKey(typeofEvent)) return;
            Action<IDomainEvent> action = evnt => handler((TEvent)evnt);
            _handlers.Add(typeofEvent, action);
        }

        public void RegisterHandler<TEvent>(Action<TEvent> handler) where TEvent : IDomainEvent
        {
            if (_handlers.ContainsKey(typeof(TEvent))) return;
            Action<IDomainEvent> action = evnt => handler((TEvent)evnt);
            _handlers.Add(typeof(TEvent), action);
        }

        public void UnregisterExecutor<TEvent>() where TEvent : IDomainEvent
        {
            _handlers.Remove(typeof(TEvent));
        }

        private Action<IDomainEvent> GetEventHandlerForEvent(Type typeOfEvent)
        {
            Action<IDomainEvent> result;
            _handlers.TryGetValue(typeOfEvent, out result);

            return result;
        }

        public void Dispose()
        {
        }
    }
}