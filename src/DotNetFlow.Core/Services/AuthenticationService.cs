using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;

namespace DotNetFlow.Core.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _repository;
        private readonly IHashPasswords _passwordHashing;

        public AuthenticationService(IUserRepository repository, IHashPasswords passwordHashing)
        {
            _repository = repository;
            _passwordHashing = passwordHashing;
        }

        public AuthenticationModel Authenticate(string email, string password)
        {
            var user = _repository.FindByEmail(email);
            if (user != null)
            {
                // Compare hash of submitted password with that stored for the user, using the same salt                
                if (_passwordHashing.Verify(password, user.HashedPassword))
                    return AuthenticationModel.Authorised(user);
            }

            return AuthenticationModel.NotAuthorised();
        }
    }
}