﻿using FluentValidation;

namespace DotNetFlow.Core.Commands.Validators
{
    public sealed class LoginUserValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty().Length(1, 1000);
            RuleFor(x => x.Password).NotEmpty().Length(6, 1000);
        }
    }
}