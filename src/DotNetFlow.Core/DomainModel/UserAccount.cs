using System;
using DotNetFlow.Core.Events;
using Ncqrs.Domain;

namespace DotNetFlow.Core.DomainModel
{
    public sealed class UserAccount : AggregateRootMappedByConvention
    {
        private Guid _id;
        private string _fullName, _email, _website, _twitter;
        private DateTime _registeredAt;
        private HashedPassword _password;

        public UserAccount(Guid id, string fullName, string email, string password, string website, string twitter)
        {
            var hashedPassword = HashedPassword.Create(password);

            ApplyEvent(new UserAccountRegisteredEvent
            {
                UserId = id,
                RegisteredAt = DateTime.Now,
                FullName = fullName,
                Email = email,
                HashedPassword = hashedPassword.Hashed,
                PasswordSalt = hashedPassword.Salt,
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
            _password = new HashedPassword(@event.HashedPassword, @event.PasswordSalt);
            _website = @event.Website;
            _twitter = @event.Twitter;
        }
    }
}