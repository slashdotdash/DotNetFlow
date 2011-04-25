using System.Collections.Generic;
using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.DomainModel;
using DotNetFlow.Core.Events;
using DotNetFlow.Specifications.Infrastructure;
using Ncqrs.Eventing.Sourcing;
using Ncqrs.Commanding;
using NUnit.Framework;

namespace DotNetFlow.Specifications
{
    [Specification]
    public sealed class AnonymousUserSubmitsNewItemSpec : AggregateRootSpecification<Item>
    {
        private const string SubmittingUsersName = "Anonymous";
        private const string Title = "Announcing release of ASP.NET MVC 3, IIS Express, SQL CE 4, Web Farm Framework, Orchard, WebMatrix";
        private const string Content = "Scott Gu has <a href=\"http://weblogs.asp.net/scottgu/archive/2011/01/13/announcing-release-of-asp-net-mvc-3-iis-express-sql-ce-4-web-farm-framework-orchard-webmatrix.aspx\">announced the release of ASP.NET MVC 3</a>";

        protected override IEnumerable<ISourcedEvent> Given()
        {
            yield break;
        }

        protected override ICommand When()
        {
            return new SubmitNewItemCommand(SubmittingUsersName, Title, Content);
        }

        [Then]
        public void Should_Fire_NewItemSubmitted_Event()
        {
            Assert.IsInstanceOf(typeof(NewItemSubmittedEvent), Events.Single());
        }

        [Then]
        public void Should_Mark_New_Item_As_Pending_Approval()
        {
            var @event = (NewItemSubmittedEvent)Events.Single();
            Assert.AreEqual(ApprovalStatus.Pending, @event.Status);
        }
    }
}
