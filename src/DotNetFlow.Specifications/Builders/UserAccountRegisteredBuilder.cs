using System;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Specifications.Builders
{
    internal sealed class UserAccountRegisteredBuilder
    {
        private Guid _id = Guid.NewGuid();
        private DateTime _registeredAt = DateTime.Now;
        private string _fullName = "Ben Smith";
        private string _email = "ben@dotnetflow.com";
        private string _hashedPassword = "password1234";
        private string _passwordSalt = "salt";
        private string _website = "www.dotnetflow.com";
        private string _twitter = "dotnetflow";

        public UserAccountRegisteredBuilder Id(Guid id)
        {
            _id = id;
            return this;
        }

        public UserAccountRegisteredBuilder Named(string name)
        {
            _fullName = name;
            return this;
        }

        public UserAccountRegisteredBuilder Email(string email)
        {
            _email = email;
            return this;
        }

        public UserAccountRegisteredBuilder Password(string hashed, string salt)
        {
            _hashedPassword = hashed;
            _passwordSalt = salt;
            return this;
        }

        public UserAccountRegisteredEvent Build()
        {
            return new UserAccountRegisteredEvent
            {
                UserId = _id,
                RegisteredAt = _registeredAt,
                FullName = _fullName,
                Email = _email,
                HashedPassword = _hashedPassword,
                PasswordSalt = _passwordSalt,
                Website = _website,
                Twitter = _twitter
            };
        }
    }
}