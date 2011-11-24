using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Commands.Executors;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.Infrastructure.Commanding;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;
using NUnit.Framework;

namespace DotNetFlow.Specifications.RegisteringNewUserAccount
{
    [Specification]
    public sealed class AnonymousUserRegistersAnAccountSpec : CommandTestFixture<RegisterUserAccountCommand>
    {
        protected override RegisterUserAccountCommand WhenExecuting()
        {
            return new RegisterUserAccountBuilder().Build();
        }

        protected override ICommandExecutor<RegisterUserAccountCommand> BuildCommandExecutor()
        {
            return new RegisterUserAccountExecutor();
        }

        [Then]
        public void Should_Publish_NewItemSubmitted_Event()
        {
            Assert.IsInstanceOf(typeof(UserAccountRegisteredEvent), CommittedEvents.Single().Body);
        }

        [And]
        public void Should_Set_EventSourceId_As_UserId()
        {
            var @event = (UserAccountRegisteredEvent)CommittedEvents.Single().Body;
            Assert.AreEqual(ExecutedCommand.UserId, @event.UserId);
        }

        [And]
        public void Should_Set_Event_Properties()
        {
            var @event = (UserAccountRegisteredEvent)CommittedEvents.Single().Body;
            Assert.AreEqual(ExecutedCommand.UserId, @event.UserId);
            Assert.AreEqual(ExecutedCommand.Username, @event.Username);
            Assert.AreEqual(ExecutedCommand.FullName, @event.FullName);
            Assert.AreEqual(ExecutedCommand.Email, @event.Email);
            Assert.AreEqual(ExecutedCommand.Password, @event.HashedPassword);
        }
    }
}