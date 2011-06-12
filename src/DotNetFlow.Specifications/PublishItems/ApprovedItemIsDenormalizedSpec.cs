using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;
using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.ReadModel.Denormalizers;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace DotNetFlow.Specifications.PublishItems
{
    [Specification, Integration]
    public sealed class ApprovedItemIsDenormalizedSpec : EventDenormalizerTestFixture<ItemPublishedEvent>
    {
        protected override ItemPublishedEvent WhenExecutingEvent()
        {
            return new ItemPublishedBuilder().Build();
        }

        protected override IEventHandler<ItemPublishedEvent> BuildEventHandler()
        {
            return new PublishedItemDenormalizer(UnitOfWork);
        }

        [Then]
        public void Should_Insert_PublishedItem()
        {
            var items = UnitOfWork.Connection.Query<int>("select count(*) from Items", null, UnitOfWork.Transaction);
            Assert.AreEqual(1, items.Single());
        }
    }
}