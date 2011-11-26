using System.Text.RegularExpressions;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Services;
using DotNetFlow.Features.Infrastructure;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class AccountRegistrationSteps : TechTalk.SpecFlow.Steps
    {        
        [Given(@"a user account has been registered")]
        public void GivenAUserAccountHasBeenRegistered()
        {
            var fullName = Faker.Name.FullName(Faker.NameFormats.Standard);
            var userName = Regex.Replace(fullName, @"[^A-Za-z0-9]|\s", string.Empty);
            var email = Faker.Internet.Email(fullName);

            CreateUserAccount(fullName, userName, email);
        }

        [Given(@"a user account with approval permission has been registered")]
        public void GivenAUserAccountWithApprovalPermissionHasBeenRegistered()
        {
            var fullName = Faker.Name.FullName(Faker.NameFormats.Standard);

            CreateUserAccount(fullName, "approver", "approver@dotnetflow.com");

            // TODO: Grant approval permission
        }

        [When(@"I complete the required fields for registration")]
        public void WhenICompleteTheRequiredFieldsForRegistration()
        {
            var fullName = Faker.Name.FullName(Faker.NameFormats.Standard);
            var userName = Regex.Replace(fullName, @"[^A-Za-z0-9]|\s", string.Empty);
            var email = Faker.Internet.Email(fullName);

            When(string.Format("I fill in \"FullName\" with \"{0}\"", fullName));
            When(string.Format("I fill in \"Username\" with \"{0}\"", userName));
            When(string.Format("I fill in \"Email\" with \"{0}\"", email));
            When("I fill in \"Password\" with \"password\"");

            ScenarioContext.Current.Set(userName, "Username");
        }

        [When(@"I attempt to register an account with an existing email address")]
        public void WhenIAttemptToRegisterAnAccountWithAnExistingEmailAddress()
        {
            var command = ScenarioContext.Current.Get<RegisterUserAccountCommand>();

            var fullName = Faker.Name.FullName(Faker.NameFormats.Standard);
            var userName = Regex.Replace(fullName, @"[^A-Za-z0-9]|\s", string.Empty);

            When(string.Format("I fill in \"FullName\" with \"{0}\"", fullName));
            When(string.Format("I fill in \"Username\" with \"{0}\"", userName));
            When(string.Format("I fill in \"Email\" with \"{0}\"", command.Email));
            When("I fill in \"Password\" with \"password\"");
        }

        [Then(@"I should see the error message ""Email address has already been registered""")]
        public void ThenIShouldSeeTheErrorMessageEmailAddressHasAlreadyBeenRegistered()
        {
            var error = WebBrowser.Current.FindElement(By.CssSelector(".field-validation-error"));
            Assert.AreEqual("Email address has already been registered", error.Text);
        }

        private static void CreateUserAccount(string fullName, string userName, string email)
        {
            var idGenerator = ScenarioContext.Current.Get<IUniqueIdentifierGenerator>();
            var passwordHashing = ScenarioContext.Current.Get<IHashPasswords>();
            
            var registerUserCommand = new RegisterUserAccountCommand
            {
                UserId = idGenerator.GenerateNewId(),
                FullName = fullName,
                Username = userName,
                Email = email,
                Password = passwordHashing.HashPassword("password")
            };

            new CommandExecutor().Execute(registerUserCommand);

            ScenarioContext.Current.Set(registerUserCommand);
            ScenarioContext.Current.Set("Username", userName);
        }
    }
}