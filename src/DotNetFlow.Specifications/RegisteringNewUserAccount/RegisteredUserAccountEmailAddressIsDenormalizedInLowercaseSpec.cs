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
    /// <summary>
    /// To ensure optimum index performance when checking for existing email address (case insensitive) it should be stored in lowercase
    /// </summary>
    [Specification, Integration]
    public sealed class RegisteredUserAccountEmailAddressIsDenormalizedInLowercaseSpec : EventDenormalizerTestFixture<UserAccountRegisteredEvent>
    {
        protected override UserAccountRegisteredEvent WhenExecutingEvent()
        {
            return new UserAccountRegisteredBuilder().Email("Email@WithUpperAndLowerCase.com").Build();
        }

        protected override IEventHandler<UserAccountRegisteredEvent> BuildEventHandler()
        {
            return new UserAccountDenormalizer(UnitOfWork);
        }       

        [Then]
        public void Should_Insert_Email_Address_In_Lowercase()
        {
            var emailAddresses = UnitOfWork.Connection.Query<string>("select Email from RegisteredEmailAddresses", null, UnitOfWork.Transaction);
            Assert.AreEqual(ExecutedEvent.Email.ToLower(), emailAddresses.Single());
        }
    }
}