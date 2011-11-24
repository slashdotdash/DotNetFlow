using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Features.Infrastructure;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class PublishingSteps : TechTalk.SpecFlow.Steps
    {
        [Given(@"an anonymous visitor has submitted an item")]
        public void GivenAnAnonymousVisitorHasSubmittedAnItem()
        {
            var idGenerator = ScenarioContext.Current.Get<IUniqueIdentifierGenerator>();

            var submitNewItemCommand = new SubmitNewItemCommand
            {
                ItemId = idGenerator.GenerateNewId(),
                Title = Faker.Lorem.Sentence(),
                UsersName = Faker.Name.FullName(),
                Content = Faker.Lorem.Paragraph(),
            };

            new CommandExecutor().Execute(submitNewItemCommand);

            ScenarioContext.Current.Set(submitNewItemCommand);
        }        

        [Given(@"I am on the submissions pending approval page")]
        public void GivenIAmOnTheSubmissionsPendingApprovalPage()
        {
            When("I navigate to /admin/pending");
        }

        [When(@"I approve the submission")]
        public void WhenIApproveTheSubmission()
        {
            When("I press \"Approve\"");
        }

        [Then(@"I should see the message ""Submission approved""")]
        public void ThenIShouldSeeTheMessageSubmissionApproved()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the submitted item should be removed from the pending approval list")]
        public void ThenTheSubmittedItemShouldBeRemovedFromThePendingApprovalList()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the approved item should appear on the home page")]
        public void ThenTheApprovedItemShouldAppearOnTheHomePage()
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"the published date should be set as today")]
        public void ThenThePublishedDateShouldBeSetAsToday()
        {
            ScenarioContext.Current.Pending();
        }
    }
}