using System;
using System.Linq;
using DotNetFlow.Core.Commands;
using DotNetFlow.Core.Commands.Validators;
using DotNetFlow.Specifications.Builders;
using FluentValidation.Results;
using Ncqrs.Spec;
using NUnit.Framework;

namespace DotNetFlow.Specifications.RegisteringNewUserAccount
{
    public class OnlyAlphanumericsAndUnderscoreAllowedInUsernameSpec : BaseTestFixture<RegisterUserAccountValidator>
    {
        private ValidationResult _validationOutcome;

        protected override void Given()
        {
            SubjectUnderTest = new RegisterUserAccountValidator(username => false, email => false);
        }

        protected override void When()
        {
            _validationOutcome = SubjectUnderTest.Validate(
                new RegisterUserAccountBuilder().Username("Inv@l!d").Build());
        }

        [Then]
        public void Should_Fail_Validation()
        {
            Assert.IsFalse(_validationOutcome.IsValid);
        }

        [Then]
        public void Should_Add_Validation_Error_For_Username()
        {
            var error = _validationOutcome.Errors.Single();
            Assert.AreEqual("Only letters, numbers and underscores are allowed", error.ErrorMessage);
            Assert.AreEqual("Username", error.PropertyName);
        }
    }
}