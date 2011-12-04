using System;
using DotNetFlow.Features.Infrastructure;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class BrowserSteps
    {
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

        //[When(@"I have filled out the form as follows")]
        //public void WhenIHaveFilledOutTheFormAsFollows(Table table)
        //{
        //    foreach (var row in table.Rows)
        //    {
        //        var label = row["Label"];
        //        var value = row["Value"];

        //        var input = WebBrowser.Current.FindElement(By.CssSelector(string.Format("label:contains('{0}') + :input", label)));
        //        input.SendKeys(value);
        //    }
        //}

        [Given(@"I have filled out the form as follows:")]
        [When(@"I have filled out the form as follows:")]
        public void GivenIHaveFilledOutTheFormAsFollows(Table table)
        {
            foreach (var row in table.Rows)
            {
                var name = row["Field"];
                var value = row["Value"];

                var input = WebBrowser.Current.FindElement(By.Name(name));
                input.SendKeys(value);                
            }
        }

        [When(@"I fill in ""(.*)"" with ""(.*)""")]
        public void WhenIFillIn(string name, string value)
        {            
            var input = WebBrowser.Current.FindElement(By.Name(name));
            input.SendKeys(value);
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