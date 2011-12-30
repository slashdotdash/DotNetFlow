using System;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Features.Infrastructure;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class PublishingSteps : TechTalk.SpecFlow.Steps
    {
        [Given(@"an anonymous visitor has submitted an item")]
        public void GivenAnAnonymousVisitorHasSubmittedAnItem()
        {
            SubmitItem();
        }        

        [Given(@"I am on the submissions pending approval page")]
        public void GivenIAmOnTheSubmissionsPendingApprovalPage()
        {
            When("I navigate to /admin/pending");
        }

        [Given(@"an item with the title ""(.*)"" has been published")]
        public void GivenAnItemWithTheTitleHasBeenPublished(string title)
        {
            SubmitItem(title: title);
            Given("I am on the submissions pending approval page");
            When("I approve the submission");
        }

        [When(@"I approve the submission")]
        public void WhenIApproveTheSubmission()
        {
            When("I press \"Approve\"");
        }

        [Then(@"I should see the notification message ""Submission approved""")]
        public void ThenIShouldSeeTheNotificationMessageSubmissionApproved()
        {
            AssertFlashMessage("Submission approved");
        }

        [Then(@"the submitted item should be removed from the pending approval list")]
        public void ThenTheSubmittedItemShouldBeRemovedFromThePendingApprovalList()
        {
            var pending = WebBrowser.Current.FindElements(By.CssSelector("table#pending-approval tbody tr"));
            Assert.AreEqual(0, pending.Count);
        }

        private static void AssertFlashMessage(string message)
        {
            var flash = WebBrowser.Current.WaitForElement(By.CssSelector("#flash-messages p"));
            Assert.AreEqual(message, flash.Text);
        }

        [Then(@"the approved item should appear on the home page")]
        public void ThenTheApprovedItemShouldAppearOnTheHomePage()
        {
            When("I navigate to /");
        }

        [Then(@"the published date should be set as today")]
        public void ThenThePublishedDateShouldBeSetAsToday()
        {
            ScenarioContext.Current.Pending();
        }

        private static void SubmitItem(string title = null, string content = null, string submittedByUser = null)
        {
            var idGenerator = ScenarioContext.Current.Get<IUniqueIdentifierGenerator>();

            var submitNewItemCommand = new SubmitNewItemCommand
            {
                ItemId = idGenerator.GenerateNewId(),
                Title = title ?? Faker.Lorem.Sentence(),
                Content = content ?? Faker.Lorem.Paragraph(),
                FullName = submittedByUser ?? Faker.Name.FullName(),
                SubmittedAt = DateTime.UtcNow,
            };

            new CommandExecutor().Execute(submitNewItemCommand);

            ScenarioContext.Current.Set(submitNewItemCommand);
        }        
    }
}