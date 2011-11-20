using System;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.Infrastructure.Eventing;

namespace DotNetFlow.Core.Events
{
    public sealed class UserAccountRegisteredEvent : IDomainEvent
    {        
        public Guid UserId { get; set; }
        public DateTime RegisteredAt;
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Twitter { get; set; }
        public string HashedPassword { get; set; }
    }
}