using System.Linq;
using System.Web.Security;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Commands.Executors;
using DotNetFlow.Core.Events;
using DotNetFlow.Specifications.Builders;
using Ncqrs.Commanding.CommandExecution;
using Ncqrs.Spec;
using NUnit.Framework;

namespace DotNetFlow.Specifications.RegisteringNewUserAccount
{
    [Specification]
    public sealed class AnonymousUserRegistersAnAccountSpec : CommandTestFixture<RegisterUserAccountCommand>
    {
        protected override RegisterUserAccountCommand WhenExecutingCommand()
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
            Assert.IsInstanceOf(typeof(UserAccountRegisteredEvent), PublishedEvents.Single());
        }
       
        [And]
        public void Should_Set_Event_Properties()
        {
            var @event = (UserAccountRegisteredEvent)PublishedEvents.Single();
            Assert.AreEqual(ExecutedCommand.UserId, @event.UserId);
            Assert.AreEqual(ExecutedCommand.FullName, @event.FullName);
            Assert.AreEqual(ExecutedCommand.Email, @event.Email);
        }

        [And]
        public void Should_Hash_Password_With_Salt()
        {
            var @event = (UserAccountRegisteredEvent)PublishedEvents.Single();
            Assert.IsNotEmpty(@event.PasswordSalt);

            var hashedPassword = HashPassword(ExecutedCommand.Password, @event.PasswordSalt);
            Assert.AreEqual(hashedPassword, @event.HashedPassword);
        }

        private static string HashPassword(string password, string salt)
        {
            var saltAndPwd = string.Concat(password, salt);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
        }
    }
}