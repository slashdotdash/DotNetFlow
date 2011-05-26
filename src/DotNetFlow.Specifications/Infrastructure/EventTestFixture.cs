using System;
using Ncqrs.Eventing.ServiceModel.Bus;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Spec;

namespace DotNetFlow.Specifications.Infrastructure
{
    [Specification]
    public abstract class EventTestFixture<TEvent> where TEvent : ISourcedEvent
    {
        protected Exception CaughtException { get; private set; }

        protected TEvent ExecutedEvent { get; private set; }

        protected abstract TEvent WhenExecutingEvent();

        protected virtual void SetupDependencies() { }
        protected virtual void Finally() { }

        [Given]
        public void Setup()
        {            
            SetupDependencies();
            try
            {
                var handler = BuildEventHandler();

                var @event = WhenExecutingEvent();

                handler.Handle(@event);

                ExecutedEvent = @event;
            }
            catch (Exception exception)
            {
                CaughtException = exception;
            }
            finally
            {
                Finally();
            }
        }

        protected abstract IEventHandler<TEvent> BuildEventHandler();
    }
}