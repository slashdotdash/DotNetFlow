using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Commands.Executors;
using DotNetFlow.Core.Events;
using DotNetFlow.Specifications.Builders;
using Ncqrs.Commanding;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Spec;
using NUnit.Framework;

namespace DotNetFlow.Specifications.SubmittingNewItems
{
    [Specification]
    public sealed class SubmittedItemContentIsConvertedToHtmlSpec : CommandTestFixture<SubmitNewItemCommand>
    {
        protected override ICommandExecutor<ICommand> BuildCommandExecutor()
        {
            return (ICommandExecutor<ICommand>)new SubmitNewItemExecutor();
        }

        protected override SubmitNewItemCommand WhenExecuting()
        {
            return new SubmitNewItemBuilder().Content("Have you visited [example](http://www.example.com) before?").Build();
        }

        [Then]
        public void Should_Convert_Content_As_Markdown_To_Html()
        {
            var @event = (NewItemSubmittedEvent)PublishedEvents.Single().Payload;
            Assert.AreEqual("<p>Have you visited <a href=\"http://www.example.com\">example</a> before?</p>\n", @event.HtmlContent);
        }
    }
}