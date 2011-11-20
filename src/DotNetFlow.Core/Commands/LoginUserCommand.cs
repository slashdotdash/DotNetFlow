using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class LoginUserCommand : ICommand
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}