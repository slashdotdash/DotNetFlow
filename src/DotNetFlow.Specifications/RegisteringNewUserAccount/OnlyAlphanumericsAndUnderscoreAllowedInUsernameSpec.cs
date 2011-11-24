using System.Linq;
using DotNetFlow.Core.Commands.Validators;
using DotNetFlow.Specifications.Builders;
using DotNetFlow.Specifications.Infrastructure;
using FluentValidation.Results;
using NUnit.Framework;

namespace DotNetFlow.Specifications.RegisteringNewUserAccount
{
    [Specification]
    public class OnlyAlphanumericsAndUnderscoreAllowedInUsernameSpec : SpecificationBase
    {
        private ValidationResult _validationOutcome;
        private RegisterUserAccountValidator _subjectUnderTest;

        public override void Given()
        {
            _subjectUnderTest = new RegisterUserAccountValidator(username => false, email => false);
        }

        public override void When()
        {
            _validationOutcome = _subjectUnderTest.Validate(
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