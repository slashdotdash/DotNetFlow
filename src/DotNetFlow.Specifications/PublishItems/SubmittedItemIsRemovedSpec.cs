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
    public sealed class SubmittedItemIsRemovedSpec : EventDenormalizerTestFixture<ItemPublishedEvent>
    {
        protected override ItemPublishedEvent WhenExecutingEvent()
        {
            return new ItemPublishedBuilder().Build();
        }

        protected override IEventHandler<ItemPublishedEvent> BuildEventHandler()
        {
            return new SubmittedItemDenormalizer(UnitOfWork);
        }

        [Then]
        public void Should_Delete_SubmittedItem()
        {
            var submissions = UnitOfWork.Connection.Query<int>("select count(*) from Submissions", null, UnitOfWork.Transaction);
            Assert.AreEqual(0, submissions.Single());
        }
    }
}