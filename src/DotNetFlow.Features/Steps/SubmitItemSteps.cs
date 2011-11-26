using DotNetFlow.Features.Infrastructure;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class SubmitItemSteps : TechTalk.SpecFlow.Steps
    {
        [Then(@"I should be redirected to view the submitted item")]
        public void ThenIShouldBeRedirectedToViewTheSubmittedItem()
        {
            Then(@"I should be on the your submission page");
        }

        [Then(@"I should see the message ""(.*)""")]
        public void ThenIShouldSeeTheMessageThank_YouForSubmittingANewItemItIsNowPendingApproval(string expected)
        {
            var message = WebBrowser.Current.FindElement(By.CssSelector(".message"));
            Assert.AreEqual(expected, message.Text);
        }
    }
}