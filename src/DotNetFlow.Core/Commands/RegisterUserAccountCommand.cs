using System;
using DotNetFlow.Core.Infrastructure.Commanding;

namespace DotNetFlow.Core.Commands
{
    public sealed class RegisterUserAccountCommand : ICommand
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
    }
}