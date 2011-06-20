using System;
using DotNetFlow.Core.ReadModel.Repositories;
using FluentValidation;

namespace DotNetFlow.Core.Commands.Validators
{
    public sealed class RegisterUserAccountValidator : AbstractValidator<RegisterUserAccountCommand>
    {
        public RegisterUserAccountValidator(Func<IRegisteredEmailRepository> emailRepository)
        {
            RuleFor(x => x.FullName).NotEmpty().Length(1, 200);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().Length(1, 1000);
            RuleFor(x => x.Password).NotEmpty().Length(6, 1000);

            // Optional
            RuleFor(x => x.Website).Length(0, 1000);
            RuleFor(x => x.Twitter).Length(0, 200);

            // Enforce unique email address when entered
            RuleFor(x => x.Email).Must(email => BeUniqueEmailAddress(emailRepository(), email))
                .Unless(x => string.IsNullOrWhiteSpace(x.Email))
                .WithMessage("Email address has already been registered");
        }

        /// <summary>
        /// Prevent the same email address from being registered more than once (since this is also the login so must be unique)
        /// </summary>
        private static bool BeUniqueEmailAddress(IRegisteredEmailRepository repository, string email)
        {
            return !repository.Exists(email);
        }
    }
}