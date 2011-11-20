using Dapper;
using DotNetFlow.Core.Events;
using DotNetFlow.Core.Infrastructure;

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
            RegisterUsername(evnt);
            RegisterEmailAddress(evnt);
        }

        private void CreateUser(UserAccountRegisteredEvent evnt)
        {
            _context.Connection.Execute(
                "insert into Users (UserId, RegisteredAt, FullName, Username, Email, HashedPassword, Website, Twitter) values (@UserId, @RegisteredAt, @FullName, @Username, @Email, @HashedPassword, @Website, @Twitter)",
                new { evnt.UserId, evnt.RegisteredAt, evnt.FullName, evnt.Username, evnt.Email, evnt.HashedPassword, evnt.Website, evnt.Twitter },
                _context.Transaction);
        }

        private void RegisterUsername(UserAccountRegisteredEvent evnt)
        {
            _context.Connection.Execute(
                "insert into RegisteredUsernames (UserId, Username) values (@UserId, @Username)",
                new { evnt.UserId, Username = evnt.Username.Trim().ToLower() }, // Username is stored in lowercase
                _context.Transaction);
        }

        private void RegisterEmailAddress(UserAccountRegisteredEvent evnt)
        {
            _context.Connection.Execute(
                "insert into RegisteredEmailAddresses (UserId, Email) values (@UserId, @Email)",
                new { evnt.UserId, Email = evnt.Email.Trim().ToLower() }, // Email address is stored in lowercase
                _context.Transaction);
        }
    }
}