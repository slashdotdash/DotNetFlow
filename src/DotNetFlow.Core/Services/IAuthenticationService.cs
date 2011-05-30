using DotNetFlow.Core.ReadModel.Models;

namespace DotNetFlow.Core.Services
{
    public interface IAuthenticationService
    {
        AuthenticationModel Authenticate(string email, string password);
    }
}