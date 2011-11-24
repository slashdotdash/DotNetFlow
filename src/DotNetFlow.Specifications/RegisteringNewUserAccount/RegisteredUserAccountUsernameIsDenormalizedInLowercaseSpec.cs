using System.Linq;
using DotNetFlow.Core.Infrastructure.Eventing;
using NUnit.Framework;
using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.ReadModel.Denormalizers;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;

namespace DotNetFlow.Specifications.RegisteringNewUserAccount
{
    /// <summary>
    /// To ensure optimum index performance when checking for existing username (case insensitive) it should be stored in lowercase
    /// </summary>
    [Specification, Integration]
    public sealed class RegisteredUserAccountUsernameIsDenormalizedInLowercaseSpec : EventDenormalizerTestFixture<UserAccountRegisteredEvent>
    {
        protected override UserAccountRegisteredEvent WhenExecutingEvent()
        {
            return new UserAccountRegisteredBuilder().Username("dOtNETflow").Build();
        }

        protected override IEventHandler<UserAccountRegisteredEvent> BuildEventHandler()
        {
            return new UserAccountDenormalizer(UnitOfWork);
        }

        [Then]
        public void Should_Insert_Email_Address_In_Lowercase()
        {
            var usernames = UnitOfWork.Connection.Query<string>("select Username from RegisteredUsernames", null, UnitOfWork.Transaction);
            Assert.AreEqual(ExecutedEvent.Username.ToLower(), usernames.Single());
        }
    }
}