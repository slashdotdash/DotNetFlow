using System;
using DotNetFlow.Core.Infrastructure.Eventing;
using NUnit.Framework;

namespace DotNetFlow.Specifications.Infrastructure
{
    [Specification]
    public abstract class EventTestFixture<TEvent> where TEvent : IDomainEvent
    {
        protected Exception CaughtException { get; private set; }

        protected TEvent ExecutedEvent { get; private set; }

        protected abstract TEvent WhenExecutingEvent();

        protected virtual void SetupDependencies() { }
        protected virtual void Finally() { }

        [TestFixtureSetUp]        
        public void Setup()
        {
            SetupDependencies();
            try
            {
                var handler = BuildEventHandler();
                var evnt = WhenExecutingEvent();

                handler.Handle(evnt);

                ExecutedEvent = evnt;
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