using System;
using System.Text.RegularExpressions;
using DotNetFlow.Features.Infrastructure;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class NavigationSteps : TechTalk.SpecFlow.Steps
    {
        [Given(@"I am on the login page")]
        public void GivenIAmOnTheLoginPage()
        {
            NavigateTo("/login");
        }

        [Given(@"I am on the registration page")]
        public void GivenIAmOnTheRegistrationPage()
        {
            NavigateTo("/register");
        }

        [Given(@"I am on the submit item page")]
        public void GivenIAmOnTheSubmitItemPage()
        {
            NavigateTo("/submit");
        }

        [Then(@"I should be on the home page")]
        public void ThenIShouldBeOnTheHomePage()
        {
            Assert.AreEqual(UrlFor("/").ToString(), WebBrowser.Current.Url);
        }

        [Then(@"I should be on the your submission page")]
        public void ThenIShouldBeOnTheYourSubmissionPage()
        {
            var expected = UrlFor("/your-submission").ToString();
            var matches = Regex.IsMatch(WebBrowser.Current.Url, string.Concat(Regex.Escape(string.Concat(expected, "/")), "[a-z0-9]{8}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{4}-[a-z0-9]{12}"));
            Assert.IsTrue(matches);
        }

        private static Uri UrlFor(string relativeUrl)
        {
            var rootUrl = new Uri("http://localhost:4000/");
            return new Uri(rootUrl, relativeUrl);
        }

        public void NavigateTo(string relativeUrl)
        {
            WebBrowser.Current.Navigate().GoToUrl(UrlFor(relativeUrl));
        }
    }
}