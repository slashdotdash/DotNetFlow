using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Features.Infrastructure;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class BrowserSteps
    {
        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            WhenINavigateTo("/login");
        }

        [When(@"I navigate to (.*)")]
        public void WhenINavigateTo(string relativeUrl)
        {
            var rootUrl = new Uri("http://localhost:4000/");
            WebBrowser.Current.Navigate().GoToUrl(new Uri(rootUrl, relativeUrl));
        }

        [When(@"I press ""(.*)""")]
        public void WhenIPress(string buttonName)
        {
            var button = WebBrowser.Current.FindElement(By.CssSelector(string.Format("input[value=\"{0}\"]", buttonName)));
            if (button == null) Assert.Fail("Could not find button named '{0}'", buttonName);

            button.Click();
        }

        [When(@"I have filled out the form as follows")]
        public void WhenIHaveFilledOutTheFormAsFollows(Table table)
        {
            foreach (var row in table.Rows)
            {
                var labelText = row["Label"];
                var value = row["Value"];

                //WebBrowser.Current.FindElement(Find.ByLabelText(labelText)).TypeText(value);
            }
        }

        [Then(@"I see the flash message ""(.*)""")]
        public void ThenISeeTheFlashMessage(string message)
        {
            throw new NotImplementedException();

            //            var flashElement = WebBrowser.Current.Element("flashMessage");
            //          Assert.That(flashElement.Text, Is.EqualTo(message));
        }

        [Then(@"I should be on the ""(.*)"" page")]
        public void ThenIShouldBeOnThePage(string name)
        {
            throw new NotImplementedException();

            //            var header = WebBrowser.Current.ElementWithTag("h1", Find.ByText(name));
            //          Assert.IsTrue(header.Exists);
        }
    }
}