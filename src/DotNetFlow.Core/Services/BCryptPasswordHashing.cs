namespace DotNetFlow.Core.Services
{
    /// <summary>
    /// Why BCrypt? Most popular password storage schemes are based on fast hashing algorithms such as MD5 and SHA-1. 
    /// BCrypt is a computationally expensive adaptive hashing scheme which utilizes the Blowfish block cipher. 
    /// It is ideally suited for password storage, as its slow initialization time severely limits the effectiveness of brute force password cracking attempts. 
    /// How much overhead it adds is configurable (that's the adaptive part), so the computational resources required to test a password candidate 
    /// can grow along with advancements in hardware capabilities.
    /// </summary>
    /// <see cref="http://derekslager.com/blog/posts/2007/10/bcrypt-dotnet-strong-password-hashing-for-dotnet-and-mono.ashx"/>
    /// <seealso cref="http://bcrypt.codeplex.com/"/>
    public sealed class BCryptPasswordHashing : IHashPasswords
    {
        private readonly int _workFactor;

        public BCryptPasswordHashing(int workFactor = 10)
        {
            _workFactor = workFactor;
        }

        public string HashPassword(string plaintext)
        {
            return BCrypt.Net.BCrypt.HashPassword(plaintext, BCrypt.Net.BCrypt.GenerateSalt(_workFactor));
        }

        public bool Verify(string candidate, string hashed)
        {
            return BCrypt.Net.BCrypt.Verify(candidate, hashed);
        }
    }
}