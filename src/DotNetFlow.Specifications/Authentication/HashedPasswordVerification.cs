using DotNetFlow.Core.Services;
using Ncqrs.Spec;
using NUnit.Framework;

namespace DotNetFlow.Specifications.Authentication
{
    [Specification]
    public sealed class HashedPasswordVerification : BaseTestFixture
    {
        private IHashPasswords _passwordHashing;
        private const string Plaintext = "Plain Text Password";
        private string _hashedPassword;

        protected override void Given()
        {
            _passwordHashing = new BCryptPasswordHashing();
        }

        protected override void When()
        {
            _hashedPassword = _passwordHashing.HashPassword(Plaintext);
        }

        [Then]
        public void Should_Hash_Password()
        {
            Assert.IsNotEmpty(_hashedPassword);
            Assert.AreNotEqual(Plaintext, _hashedPassword);
        }

        [Then]
        public void Should_Verify_Password_When_Matched()
        {
            AssertMatch(Plaintext);
        }

        [Then]
        public void Should_Fail_Verification_When_Not_Matched()
        {
            AssertNotMatched("Not The Expected Plaintext");
        }

        [Then]
        public void Should_Fail_Verification_When_Empty_String()
        {
            AssertNotMatched(string.Empty);
        }

        [Then]
        public void Should_Still_Verify_Password_When_Work_Factor_Is_Changed()
        {
            var hashingWithIncreasedWorkRate = new BCryptPasswordHashing(11);
            Assert.IsTrue(hashingWithIncreasedWorkRate.Verify(Plaintext, _hashedPassword));
        }

        /// <summary>
        /// Assert that the given candidate string matches the hashed password
        /// </summary>
        private void AssertMatch(string candidate)
        {
            Assert.IsTrue(_passwordHashing.Verify(candidate, _hashedPassword));
        }

        /// <summary>
        /// Assert that the given candidate string does not match the hashed password
        /// </summary>
        private void AssertNotMatched(string candidate)
        {
            Assert.IsFalse(_passwordHashing.Verify(candidate, _hashedPassword));
        }
    }
}