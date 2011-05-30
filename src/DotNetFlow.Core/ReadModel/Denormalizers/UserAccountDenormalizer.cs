using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.Infrastructure;
using Ncqrs.Eventing.ServiceModel.Bus;

namespace DotNetFlow.Core.ReadModel.Denormalizers
{
    public sealed class UserAccountDenormalizer : IEventHandler<UserAccountRegisteredEvent>
    {
        private readonly IUnitOfWork _context;

        public UserAccountDenormalizer(IUnitOfWork unitOfWork)
        {
            _context = unitOfWork;
        }

        /// <summary>
        /// Create user account
        /// </summary>
        public void Handle(UserAccountRegisteredEvent evnt)
        {
            CreateUser(evnt);
            RegisterEmailAddress(evnt);
        }

        private void CreateUser(UserAccountRegisteredEvent evnt)
        {
            _context.Connection.Execute(
                "insert into Users (UserId, RegisteredAt, FullName, Email, HashedPassword, PasswordSalt, Website, Twitter) values (@UserId, @RegisteredAt, @FullName, @Email, @HashedPassword, @PasswordSalt, @Website, @Twitter)",
                new { evnt.UserId, evnt.RegisteredAt, evnt.FullName, evnt.Email, evnt.HashedPassword, evnt.PasswordSalt, evnt.Website, evnt.Twitter },
                _context.Transaction);
        }

        private void RegisterEmailAddress(UserAccountRegisteredEvent evnt)
        {
            _context.Connection.Execute(
                "insert into RegisteredEmailAddresses (UserId, Email) values (@UserId, @Email)",
                new { evnt.UserId, evnt.Email },
                _context.Transaction);
        }
    }
}