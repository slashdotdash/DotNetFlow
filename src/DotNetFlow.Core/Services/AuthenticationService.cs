﻿using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;

namespace DotNetFlow.Core.Services
{
    public sealed class AuthenticationService : IAuthenticationService
    {
        private readonly IUserReadModelRepository _readModelRepository;
        private readonly IHashPasswords _passwordHashing;

        public AuthenticationService(IUserReadModelRepository readModelRepository, IHashPasswords passwordHashing)
        {
            _readModelRepository = readModelRepository;
            _passwordHashing = passwordHashing;
        }

        public AuthenticationModel Authenticate(string usernameOrEmail, string password)
        {
            var user = _readModelRepository.FindByUsernameOrEmail(usernameOrEmail);
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