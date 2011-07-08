using System;
using DotNetFlow.Core.ReadModel.Queries;
using FluentValidation;

namespace DotNetFlow.Core.Commands.Validators
{
    public sealed class RegisterUserAccountValidator : AbstractValidator<RegisterUserAccountCommand>
    {
        public RegisterUserAccountValidator(Func<IFindExistingUsername> usernameQuery, Func<IFindExistingEmailAddress> emailQuery)
        {
            RuleFor(x => x.FullName).NotEmpty().Length(1, 200);
            RuleFor(x => x.Username).NotEmpty().Length(1, 20);
            RuleFor(x => x.Username).Must(username => BeUniqueUsername(usernameQuery(), username))
                .Unless(x => string.IsNullOrWhiteSpace(x.Username))
                .WithMessage("Usernames is already taken");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(1, 1000);
            RuleFor(x => x.Password).NotEmpty().Length(6, 1000);

            // Optional
            RuleFor(x => x.Website).Length(0, 1000);
            RuleFor(x => x.Twitter).Length(0, 20);  // Twitter usernames are 15 chars max length

            // Enforce unique email address when entered
            RuleFor(x => x.Email).Must(email => BeUniqueEmailAddress(emailQuery(), email))
                .Unless(x => string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Email address has already been registered");
        }

        /// <summary>
        /// Prevent the same username from being registered more than once (as this is the login so must be unique)
        /// </summary>
        private static bool BeUniqueUsername(IFindExistingUsername query, string username)
        {
            return !query.Exists(username);
        }

        /// <summary>
        /// Prevent the same email address from being registered more than once
        /// </summary>
        private static bool BeUniqueEmailAddress(IFindExistingEmailAddress query, string email)
        {
            return !query.Exists(email);
        }
    }
}