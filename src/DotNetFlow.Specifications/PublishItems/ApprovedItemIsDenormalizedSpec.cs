using System.Linq;
using DotNetFlow.Core.Infrastructure.Eventing;
using NUnit.Framework;
using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.ReadModel.Denormalizers;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;

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