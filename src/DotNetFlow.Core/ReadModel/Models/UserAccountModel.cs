using System;

namespace DotNetFlow.Core.ReadModel.Models
{
    public sealed class UserAccountModel
    {
        public Guid UserId { get; set; }
        public DateTime RegisteredAt { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
        public string PasswordSalt { get; set; }
        public string HashedPassword { get; set; }
    }
}