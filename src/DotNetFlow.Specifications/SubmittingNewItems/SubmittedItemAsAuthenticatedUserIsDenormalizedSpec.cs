using System;
using System.Linq;
using DotNetFlow.Core.Infrastructure.Eventing;
using DotNetFlow.Core.ReadModel.Models;
using NUnit.Framework;
using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.ReadModel.Denormalizers;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;

namespace DotNetFlow.Specifications.SubmittingNewItems
{
    [Specification, Integration]
    public sealed class SubmittedItemAsAuthenticatedUserIsDenormalizedSpec : EventDenormalizerTestFixture<NewItemSubmittedEvent>
    {
        protected override NewItemSubmittedEvent WhenExecutingEvent()
        {
            return new NewItemSubmittedBuilder().SubmittedByRegisteredUser(Guid.NewGuid(), "dotnetflow", Faker.Name.FullName()).Build();
        }

        protected override IEventHandler<NewItemSubmittedEvent> BuildEventHandler()
        {
            return new SubmittedItemDenormalizer(UnitOfWork);
        }

        [Then]
        public void Should_Insert_SubmittedItem()
        {
            var submissions = UnitOfWork.Connection.Query<int>("select count(*) from Submissions", null, UnitOfWork.Transaction);
            Assert.AreEqual(1, submissions.Single());
        }

        [Then]
        public void Should_Record_User_Details()
        {
            var submission = UnitOfWork.Connection.Query<Submission>("select top 1 * from Submissions", null, UnitOfWork.Transaction).Single();

            Assert.AreEqual(ExecutedEvent.UserId, submission.UserId);
            Assert.AreEqual("dotnetflow", submission.Username);
            Assert.AreEqual(ExecutedEvent.FullName, submission.FullName);
        }
    }
}