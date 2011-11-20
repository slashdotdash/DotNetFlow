using System;
using System.Collections.Generic;
using EventStore;
using EventStore.Dispatcher;
using StructureMap;

namespace DotNetFlow.Core.Infrastructure.Eventing
{
    public sealed class EventDispatcher : IDispatchCommits
    {
        private readonly Dictionary<Type, Action<IUnitOfWork, IDomainEvent>> _handlers = new Dictionary<Type, Action<IUnitOfWork, IDomainEvent>>();

        public void Dispatch(Commit commit)
        {
            var uow = ObjectFactory.GetInstance<IUnitOfWork>();

            foreach (var @event in commit.Events)
            {
                Dispatch(uow, (IDomainEvent)@event.Body);
            }
        }

        private void Dispatch(IUnitOfWork uow, IDomainEvent @event)
        {
            var typeOfEvent = @event.GetType();
            var handler = GetEventHandlerForEvent(typeOfEvent);

            handler.Invoke(uow, @event);
        }

        public void RegisterHandler(Type typeofEvent, Func<IUnitOfWork, IEventHandler<IDomainEvent>> handler)
        {
            RegisterHandler<IDomainEvent>(typeofEvent, handler);
        }

        public void RegisterHandler<TEvent>(Type typeofEvent, Func<IUnitOfWork, IEventHandler<TEvent>> handler) where TEvent : IDomainEvent
        {
            if (_handlers.ContainsKey(typeofEvent)) return;
            Action<IUnitOfWork, IDomainEvent> action = (uow, evnt) => handler(uow).Handle((TEvent)evnt);
            _handlers.Add(typeofEvent, action);
        }

        public void RegisterHandler<TEvent>(Func<IUnitOfWork, IEventHandler<TEvent>> handler) where TEvent : IDomainEvent
        {
            if (_handlers.ContainsKey(typeof(TEvent))) return;
            Action<IUnitOfWork, IDomainEvent> action = (uow, evnt) => handler(uow).Handle((TEvent)evnt);
            _handlers.Add(typeof(TEvent), action);
        }

        public void UnregisterExecutor<TEvent>() where TEvent : IDomainEvent
        {
            _handlers.Remove(typeof(TEvent));
        }

        private Action<IUnitOfWork, IDomainEvent> GetEventHandlerForEvent(Type typeOfEvent)
        {
            Action<IUnitOfWork, IDomainEvent> result;
            _handlers.TryGetValue(typeOfEvent, out result);

            return result;
        }

        public void Dispose()
        {
        }
    }
}