using System;

namespace DotNetFlow.Specifications.Infrastructure
{
    //[Specification]
    //public abstract class EventTestFixture<TEvent> where TEvent : ISourcedEvent
    //{
    //    protected Exception CaughtException { get; private set; }

    //    protected TEvent ExecutedEvent { get; private set; }

    //    protected abstract TEvent WhenExecutingEvent();

    //    protected virtual void SetupDependencies() { }
    //    protected virtual void Finally() { }

    //    [Given]
    //    public void Setup()
    //    {            
    //        SetupDependencies();
    //        try
    //        {
    //            var handler = BuildEventHandler();

    //            var @event = WhenExecutingEvent();

    //            var published = new PublishedEvent<TEvent>(new CommittedEvent(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), 1, DateTime.Now, @event, new Version(1, 0)));

    //            handler.Handle(published);

    //            ExecutedEvent = @event;
    //        }
    //        catch (Exception exception)
    //        {
    //            CaughtException = exception;
    //        }
    //        finally
    //        {
    //            Finally();
    //        }
    //    }

    //    protected abstract IEventHandler<TEvent> BuildEventHandler();
    //}
}