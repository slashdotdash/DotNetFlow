using System;
using DotNetFlow.Core.Events;
using Ncqrs.Domain;

namespace DotNetFlow.Core.DomainModel
{
    public sealed class UserAccount : AggregateRootMappedByConvention
    {
        private Guid _id;
        private string _fullName, _hashedPassword, _email, _website, _twitter;
        private DateTime _registeredAt;        

        public UserAccount(Guid id, string fullName, string email, string hashedPassword, string website, string twitter)
        {
            ApplyEvent(new UserAccountRegisteredEvent
            {
                UserId = id,
                RegisteredAt = DateTime.Now,
                FullName = fullName,
                Email = email,
                HashedPassword = hashedPassword,                
                Website = website,
                Twitter = twitter
            });
        }

        private void OnNewItemSubmitted(UserAccountRegisteredEvent @event)
        {
            _id = @event.UserId;
            _registeredAt = @event.RegisteredAt;
            _fullName = @event.FullName;
            _email = @event.Email;
            _hashedPassword = @event.HashedPassword;
            _website = @event.Website;
            _twitter = @event.Twitter;
        }
    }
}