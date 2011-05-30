using System.Web.Security;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;

namespace DotNetFlow.Core.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;

        public AuthenticationService(IUserRepository repository)
        {
            _repository = repository;
        }

        public AuthenticationModel Authenticate(string email, string password)
        {
            var user = _repository.FindByEmail(email);
            if (user != null)
            {
                // Compare hash of submitted password with that stored for the user, using the same salt
                var hashedSubmittedPassword = HashPassword(password, user.PasswordSalt);
                if (hashedSubmittedPassword == user.HashedPassword)
                    return AuthenticationModel.Authorised(user);
            }

            return AuthenticationModel.NotAuthorised();
        }

        private static string HashPassword(string password, string salt)
        {
            var saltAndPwd = string.Concat(password, salt);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
        }
    }
}