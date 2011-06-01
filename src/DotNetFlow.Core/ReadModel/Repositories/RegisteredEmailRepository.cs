using System.Linq;
using Dapper;
using DotNetFlow.Core.Infrastructure;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public class RegisteredEmailRepository : IRegisteredEmailRepository
    {
        private readonly IUnitOfWork _context;

        public RegisteredEmailRepository(IUnitOfWork context)
        {
            _context = context;
        }

        /// <summary>
        /// is the given email address already registered?
        /// </summary>
        public bool Exists(string email)
        {
            return _context.Connection.Query<string>(
                "select Email from RegisteredEmailAddresses where Email = @Email",
                new { Email = email.ToLower() }, _context.Transaction)
                .Any();
        }
    }
}