using Ncqrs.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class LoginUserCommand : CommandBase
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}