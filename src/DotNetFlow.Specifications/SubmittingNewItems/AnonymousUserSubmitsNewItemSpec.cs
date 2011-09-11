using System;
using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Commands.Executors;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Events;
using DotNetFlow.Specifications.Builders;
using MarkdownSharp;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Spec;
using NUnit.Framework;

namespace DotNetFlow.Specifications.SubmittingNewItems
{
    [Specification]
    public sealed class AnonymousUserSubmitsNewItemSpec : CommandTestFixture<SubmitNewItemCommand>
    {
        protected override ICommandExecutor<ICommand> BuildCommandExecutor()
        {
            return (ICommandExecutor<ICommand>)new SubmitNewItemExecutor();
        }

        protected override SubmitNewItemCommand WhenExecuting()
        {
            return new SubmitNewItemBuilder().Build();
        }

        [Then]
        public void Should_Publish_NewItemSubmitted_Event()
        {
            Assert.IsInstanceOf(typeof(NewItemSubmittedEvent), PublishedEvents.Single());
        }

        [And]
        public void Should_Mark_New_Item_As_Pending_Approval()
        {
            var @event = (NewItemSubmittedEvent)PublishedEvents.Single().Payload;
            Assert.AreEqual(ApprovalStatus.Pending, @event.Status);
        }

        [And]
        public void Should_Set_EventSourceId_As_ItemId()
        {
            var @event = (NewItemSubmittedEvent)PublishedEvents.Single().Payload;
            Assert.AreEqual(ExecutedCommand.ItemId, @event.EventSourceId);
        }

        [And]
        public void Should_Set_Event_Properties()
        {
            var @event = (NewItemSubmittedEvent)PublishedEvents.Single().Payload;
            Assert.AreEqual(ExecutedCommand.ItemId, @event.ItemId);
            Assert.AreEqual(ExecutedCommand.UsersName, @event.SubmissionUsersName);
        }

        [And]
        public void Should_Convert_Raw_Content_To_Html()
        {
            var @event = (NewItemSubmittedEvent)PublishedEvents.Single().Payload;
            Assert.AreEqual(ExecutedCommand.Content, @event.RawContent);

            var parsedHtml = new Markdown().Transform(ExecutedCommand.Content);
            Assert.AreEqual(parsedHtml, @event.HtmlContent);
        }        
    }
}