using System;
using System.Collections.Generic;
using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Specifications.Builders;
using Ncqrs.Spec;
using NUnit.Framework;

namespace DotNetFlow.Specifications.PublishItems
{
    public sealed class ApproverPublishesItemSpec : OneEventTestFixture<PublishItemCommand, ItemPublishedEvent>
    {
        public ApproverPublishesItemSpec()
        {
            Bootstrapper.Configure();
        }

        protected override IEnumerable<object> GivenEvents()
        {
            yield return new NewItemSubmittedBuilder().Id(EventSourceId).Build();
        }

        protected override PublishItemCommand WhenExecuting()
        {
            return new PublishItemCommand
            {
                ApprovedBy = Guid.NewGuid(),
                ItemId = EventSourceId
            };
        }
        
        [Then]
        public void Should_Publish_ItemPublishedEvent_Event()
        {
            Assert.IsInstanceOf(typeof(ItemPublishedEvent), PublishedEvents.Single());
        }

        [And]
        public void Should_Set_Event_Properties()
        {
            var @event = (ItemPublishedEvent)PublishedEvents.Single().Payload;
            Assert.AreEqual(ExecutedCommand.ItemId, @event.EventSourceId);
            Assert.AreEqual(ExecutedCommand.ApprovedBy, @event.ApprovedBy);
        }

        [And]
        public void Should_Mark_Item_As_Approved()
        {
            var @event = (ItemPublishedEvent) PublishedEvents.Single().Payload;
            Assert.AreEqual(ApprovalStatus.Approved, @event.Status);
        }
    }
}