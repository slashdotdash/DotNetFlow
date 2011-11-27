﻿using DotNetFlow.Core.Commands;
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
            var idGenerator = ScenarioContext.Current.Get<IUniqueIdentifierGenerator>();

            var submitNewItemCommand = new SubmitNewItemCommand
            {
                ItemId = idGenerator.GenerateNewId(),
                Title = Faker.Lorem.Sentence(),
                FullName = Faker.Name.FullName(),
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
    }
}