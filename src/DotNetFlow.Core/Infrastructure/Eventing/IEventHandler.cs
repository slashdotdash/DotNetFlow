namespace DotNetFlow.Core.Infrastructure.Eventing
{
    public interface IEventHandler<in TEvent> where TEvent : IDomainEvent
    {
        void Handle(TEvent evnt);
    }
}