using System;
using System.Security.Cryptography;
using System.Web.Security;

namespace DotNetFlow.Core.DomainModel
{
    /// <summary>
    /// One-way hash of a given password string with a salt, original plain-text password is never stored
    /// </summary>
    /// <see cref="http://davidhayden.com/blog/dave/archive/2004/02/16/157.aspx"/>
    internal sealed class HashedPassword
    {
        private readonly string _hashedPassword, _passwordSalt;

        /// <summary>
        /// Create a new hashed password with a random salt
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static HashedPassword Create(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password", "Password was an empty string");

            var salt = CreateSalt(8);
            var hashed = CreatePasswordHash(password, salt);

            return new HashedPassword(hashed, salt);
        }

        public HashedPassword(string hashed, string salt)
        {
            if (string.IsNullOrWhiteSpace(hashed)) throw new ArgumentNullException("hashed", "Hashed password was empty");
            if (string.IsNullOrWhiteSpace(salt)) throw new ArgumentNullException("salt", "Password salt was empty");

            _hashedPassword = hashed;
            _passwordSalt = salt;
        }

        /// <summary>
        /// Create a cryptographic random number to use as the password salt
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string CreateSalt(int size)
        {
            if (size < 1) throw new ArgumentOutOfRangeException("size", "Salt size must be at least 1");

            //Generate a cryptographic random number.
            var cryptoProvider = new RNGCryptoServiceProvider();
            var buff = new byte[size];

            cryptoProvider.GetBytes(buff);

            // Return a Base64 string representation of the random number.
            return Convert.ToBase64String(buff);
        }

        /// <summary>
        /// Hash the given password concatenated with the salt using the SHA1 hashing algorithm
        /// </summary>
        /// <param name="pwd"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private static string CreatePasswordHash(string pwd, string salt)
        {
            var saltAndPwd = string.Concat(pwd, salt);
            return FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPwd, "sha1");
        }

        public string Hashed
        {
            get { return _hashedPassword; }
        }

        public string Salt
        {
            get { return _passwordSalt; }
        }
    }
}
