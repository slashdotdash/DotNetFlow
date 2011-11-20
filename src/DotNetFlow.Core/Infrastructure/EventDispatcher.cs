using System;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.ReadModel.Denormalizers;
using EventStore;
using EventStore.Dispatcher;
using StructureMap;

namespace DotNetFlow.Core.Infrastructure
{
    public sealed class EventDispatcher : IDispatchCommits
    {
        public void Dispatch(Commit commit)
        {
            var uow = ObjectFactory.GetInstance<IUnitOfWork>();

            foreach (var @event in commit.Events)
            {
                new PublishedItemDenormalizer(uow).Handle((ItemPublishedEvent)@event.Body);
            }
        }
        
        public void Dispose()
        {
        }
    }
}