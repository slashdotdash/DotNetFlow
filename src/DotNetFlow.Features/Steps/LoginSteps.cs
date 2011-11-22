using DotNetFlow.Core.Commands;
using DotNetFlow.Features.Infrastructure;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class LoginSteps
    {
        [When(@"I enter my username and password")]
        public void WhenIEnterMyUsernameAndPassword()
        {
            var command = ScenarioContext.Current.Get<RegisterUserAccountCommand>();
            LoginWith(command.Username, "password");
        }

        [When(@"I enter my email address and password")]
        public void WhenIEnterMyEmailAddressAndPassword()
        {
            var command = ScenarioContext.Current.Get<RegisterUserAccountCommand>();
            LoginWith(command.Email, "password");            
        }
        
        [When(@"I enter an incorrect username and password")]
        public void WhenIEnterAnIncorrectUsernameAndPassword()
        {            
            LoginWith("doesnotexist", "wrong password");
        }

        [When(@"I enter my email address and the wrong password")]
        public void WhenIEnterMyEmailAddressAndTheWrongPassword()
        {
            var command = ScenarioContext.Current.Get<RegisterUserAccountCommand>();
            LoginWith(command.Email, "wrong password");
        }
       
        [Then(@"I should see the login failed error message")]
        public void ThenIShouldSeeTheLoginFailedErrorMessage()
        {
            var error = WebBrowser.Current.FindElement(By.CssSelector(".field-validation-error"));
            Assert.AreEqual("Login failed, please check your username or e-mail address and password and try again.", error.Text);
        }

        [Then(@"I should not be logged in")]
        public void ThenIShouldNotBeLoggedIn()
        {
            AssertGuestUser();
        }

        [Then(@"I should be redirected to the home page")]
        public void ThenIShouldBeRedirectedToTheHomePage()
        {
            Assert.AreEqual(ApplicationUrl.HomePage().ToString(), WebBrowser.Current.Url);
        }

        [Then(@"I should be logged in")]
        public void ThenIShouldBeLoggedIn()
        {
            var command = ScenarioContext.Current.Get<RegisterUserAccountCommand>();
            AssertLoggedInAs(command.Username);
        }

        private static void LoginWith(string usernameOrEmail, string password)
        {
            WebBrowser.Current.FindElement(By.Id("UsernameOrEmail")).SendKeys(usernameOrEmail);
            WebBrowser.Current.FindElement(By.Id("Password")).SendKeys(password);
        }

        private static void AssertGuestUser()
        {
            AssertLoggedInAs("Guest");
        }

        private static void AssertLoggedInAs(string expectedUsername)
        {
            var loginStatus = WebBrowser.Current.FindElement(By.CssSelector(".logged-in-as"));
            Assert.IsNotNull(loginStatus, "Could not find login status element (searching for CSS class 'logged-in-as')");
            Assert.AreEqual(expectedUsername, loginStatus.Text);
        }
    }
}