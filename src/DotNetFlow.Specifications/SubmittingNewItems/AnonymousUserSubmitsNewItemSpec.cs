using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Commands.Executors;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;
using MarkdownSharp;
using NUnit.Framework;

namespace DotNetFlow.Specifications.SubmittingNewItems
{
    [Specification]
    public sealed class AnonymousUserSubmitsNewItemSpec : CommandTestFixture<SubmitNewItemCommand>
    {
        protected override ICommandExecutor<SubmitNewItemCommand> BuildCommandExecutor()
        {
            return new SubmitNewItemExecutor();
        }

        protected override SubmitNewItemCommand WhenExecuting()
        {
            return new SubmitNewItemBuilder().Build();
        }

        [Then]
        public void Should_Publish_NewItemSubmitted_Event()
        {
            Assert.IsInstanceOf(typeof(NewItemSubmittedEvent), CommittedEvents.Single().Body);
        }

        [And]
        public void Should_Mark_New_Item_As_Pending_Approval()
        {
            var @event = (NewItemSubmittedEvent)CommittedEvents.Single().Body;
            Assert.AreEqual(ApprovalStatus.Pending, @event.Status);
        }

        [And]
        public void Should_Set_EventSourceId_As_ItemId()
        {
            var @event = (NewItemSubmittedEvent)CommittedEvents.Single().Body;
            Assert.AreEqual(ExecutedCommand.ItemId, @event.ItemId);
        }

        [And]
        public void Should_Set_Event_Properties()
        {
            var @event = (NewItemSubmittedEvent)CommittedEvents.Single().Body;
            Assert.AreEqual(ExecutedCommand.ItemId, @event.ItemId);
            Assert.AreEqual(ExecutedCommand.UsersName, @event.SubmissionUsersName);
        }

        [And]
        public void Should_Convert_Raw_Content_To_Html()
        {
            var @event = (NewItemSubmittedEvent)CommittedEvents.Single().Body;
            Assert.AreEqual(ExecutedCommand.Content, @event.RawContent);

            var parsedHtml = new Markdown().Transform(ExecutedCommand.Content);
            Assert.AreEqual(parsedHtml, @event.HtmlContent);
        }
    }
}