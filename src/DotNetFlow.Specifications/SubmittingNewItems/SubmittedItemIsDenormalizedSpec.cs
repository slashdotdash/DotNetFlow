using System.Linq;
using DotNetFlow.Core.Infrastructure.Eventing;
using NUnit.Framework;
using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.ReadModel.Denormalizers;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;

namespace DotNetFlow.Specifications.SubmittingNewItems
{
    [Specification, Integration]
    public sealed class SubmittedItemIsDenormalizedSpec : EventDenormalizerTestFixture<NewItemSubmittedEvent>
    {
        protected override NewItemSubmittedEvent WhenExecutingEvent()
        {
            return new NewItemSubmittedBuilder().Build();
        }

        protected override IEventHandler<NewItemSubmittedEvent> BuildEventHandler()
        {
            return new SubmittedItemDenormalizer(UnitOfWork);
        }

        [Then]
        public void Should_Insert_SubmittedItem()
        {
            var submissions = UnitOfWork.Connection.Query<int>("select count(*) from Submissions", null, UnitOfWork.Transaction);
            Assert.AreEqual(1, submissions.Single());
        }
    }
}