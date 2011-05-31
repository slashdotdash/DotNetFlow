using System;
using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Commands.Executors;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Events;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;
using Ncqrs;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Eventing.Storage;
using Ncqrs.Spec;
using NUnit.Framework;

namespace DotNetFlow.Specifications.PublishItems
{
    public sealed class ApproverPublishesItemSpec : CommandTestFixture<PublishItemCommand>
    {
        private readonly Guid _itemId = Guid.NewGuid();

        protected override void SetupDependencies()
        {
            NcqrsEnvironment.SetDefault<IEventStore>(
                new InternalEventStore(
                    Prepare.Events(new[] { new NewItemSubmittedBuilder().Build() }).ForSource(_itemId)));
        }

        protected override PublishItemCommand WhenExecutingCommand()
        {
            return new PublishItemCommand
            {
                ApprovedBy = Guid.NewGuid(),
                ItemId = _itemId
            };
        }

        protected override ICommandExecutor<PublishItemCommand> BuildCommandExecutor()
        {
            return new PublishItemExecutor();
        }

        [Then]
        public void Should_Publish_ItemPublishedEvent_Event()
        {
            Assert.IsInstanceOf(typeof(ItemPublishedEvent), PublishedEvents.Single());
        }

        [And]
        public void Should_Set_Event_Properties()
        {
            var @event = (ItemPublishedEvent)PublishedEvents.Single();
            Assert.AreEqual(ExecutedCommand.ItemId, @event.EventSourceId);
            Assert.AreEqual(ExecutedCommand.ApprovedBy, @event.ApprovedBy);
        }

        [And]
        public void Should_Mark_Item_As_Approved()
        {
            var @event = (ItemPublishedEvent) PublishedEvents.Single();
            Assert.AreEqual(ApprovalStatus.Approved, @event.Status);
        }
    }
}