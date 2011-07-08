using System.Linq;
using Dapper;
using DotNetFlow.Core.Infrastructure;

namespace DotNetFlow.Core.ReadModel.Queries
{
    public class FindExistingEmailAddressQuery : IFindExistingEmailAddress
    {
        private readonly IUnitOfWork _context;

        public FindExistingEmailAddressQuery(IUnitOfWork context)
        {
            _context = context;
        }

        /// <summary>
        /// Is the given email address already registered?
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