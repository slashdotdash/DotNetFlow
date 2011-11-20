using System;
using CommonDomain.Core;
using DotNetFlow.Core.Events;

namespace DotNetFlow.Core.DomainModel
{
    public sealed class UserAccount : AggregateBase
    {
        private string _fullName, _username, _hashedPassword, _email, _website, _twitter;
        private DateTime _registeredAt;        

        public UserAccount(Guid id)
        {
            Id = id;
        }

        public UserAccount(Guid id, string fullName, string username, string email, string hashedPassword, string website, string twitter) : this(id)
        {
            RaiseEvent(new UserAccountRegisteredEvent
            {
                UserId = id,
                RegisteredAt = DateTime.Now,
                FullName = fullName,
                Username = username,
                Email = email,
                HashedPassword = hashedPassword,                
                Website = website,
                Twitter = twitter
            });
        }

        private void Apply(UserAccountRegisteredEvent @event)
        {
            _registeredAt = @event.RegisteredAt;
            _fullName = @event.FullName;
            _username = @event.Username;
            _email = @event.Email;
            _hashedPassword = @event.HashedPassword;
            _website = @event.Website;
            _twitter = @event.Twitter;
        }
    }
}