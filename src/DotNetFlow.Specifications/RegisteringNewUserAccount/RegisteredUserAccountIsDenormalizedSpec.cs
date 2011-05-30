using System.Linq;
using Ncqrs.Spec;
using NUnit.Framework;
using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.ReadModel.Denormalizers;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace DotNetFlow.Specifications.RegisteringNewUserAccount
{
    [Specification, Integration]
    public sealed class RegisteredUserAccountIsDenormalizedSpec : EventDenormalizerTestFixture<UserAccountRegisteredEvent>
    {
        protected override UserAccountRegisteredEvent WhenExecutingEvent()
        {
            return new UserAccountRegisteredBuilder().Build();
        }

        protected override IEventHandler<UserAccountRegisteredEvent> BuildEventHandler()
        {
            return new UserAccountDenormalizer(UnitOfWork);
        }

        [Then]
        public void Should_Insert_UserAccount()
        {
            var users = UnitOfWork.Connection.Query<int>("select count(*) from Users", null, UnitOfWork.Transaction);
            Assert.AreEqual(1, users.Single());
        }

        [Then]
        public void Should_Insert_RegisteredEmailAddress()
        {
            var emailAddresses = UnitOfWork.Connection.Query<int>("select count(*) from RegisteredEmailAddresses", null, UnitOfWork.Transaction);
            Assert.AreEqual(1, emailAddresses.Single());
        }
    }
}