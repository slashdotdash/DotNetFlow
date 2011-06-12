namespace DotNetFlow.Core.Services
{
    public interface IHashPasswords
    {
        string HashPassword(string plaintext);
        bool Verify(string candidate, string hashed);
    }
}