using System.Text.RegularExpressions;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Services;
using Ncqrs;
using Ncqrs.Commanding.ServiceModel;
using TechTalk.SpecFlow;

namespace DotNetFlow.Features.Steps
{
    [Binding]
    public class UserAccountSteps
    {
        [Given(@"a user account has been registered")]
        public void GivenAUserAccountHasBeenRegistered()
        {
            var commandService = ScenarioContext.Current.Get<ICommandService>();
            var idGenerator = ScenarioContext.Current.Get<IUniqueIdentifierGenerator>();
            var passwordHashing = ScenarioContext.Current.Get<IHashPasswords>();

            var fullName = Faker.Name.FullName(Faker.NameFormats.Standard);
            var userName = Regex.Replace(fullName, @"[^A-Za-z0-9]|\s", string.Empty);

            ScenarioContext.Current.Set("Username", userName);

            var registerUserCommand = new RegisterUserAccountCommand
            {
                UserId = idGenerator.GenerateNewId(),
                FullName = fullName,
                Username = userName,
                Email = Faker.Internet.Email(fullName),
                Password = passwordHashing.HashPassword("password")
            };

            commandService.Execute(registerUserCommand);
        }
    }
}
