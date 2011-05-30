using System;
using Ncqrs.Eventing.Sourcing;

namespace DotNetFlow.Core.Events
{
    public sealed class UserAccountRegisteredEvent : SourcedEvent
    {        
        public Guid UserId { get; set; }
        public DateTime RegisteredAt;
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
        public string PasswordSalt { get; set; }
        public string HashedPassword { get; set; }
    }
}