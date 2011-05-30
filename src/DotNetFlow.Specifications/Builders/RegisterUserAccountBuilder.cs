using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetFlow.Core.Commands;

namespace DotNetFlow.Specifications.Builders
{
    internal sealed class RegisterUserAccountBuilder
    {
        private Guid _id = Guid.NewGuid();
        private string _fullName = "Ben Smith";
        private string _email = "ben@dotnetflow.com";
        private string _password = "password1234";
        private string _website = "www.dotnetflow.com";
        private string _twitter = "dotnetflow";

        public RegisterUserAccountBuilder Id(Guid id)
        {
            _id = id;
            return this;
        }

        public RegisterUserAccountBuilder Named(string name)
        {
            _fullName = name;
            return this;
        }

        public RegisterUserAccountBuilder Email(string email)
        {
            _email = email;
            return this;
        }

        public RegisterUserAccountBuilder Password(string password)
        {
            _password = password;
            return this;
        }

        public RegisterUserAccountCommand Build()
        {
            return new RegisterUserAccountCommand
            {
                UserId = _id,
                FullName = _fullName,
                Email = _email,
                Password = _password,
                Website = _website,
                Twitter = _twitter
            };
        }
    }
}