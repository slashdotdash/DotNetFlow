using System.Text.RegularExpressions;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Services;
using DotNetFlow.Features.Infrastructure;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class UserAccountSteps
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
        }
    }
}