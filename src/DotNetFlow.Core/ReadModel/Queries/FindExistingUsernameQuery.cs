using System.Linq;
using Dapper;
using DotNetFlow.Core.Infrastructure;

namespace DotNetFlow.Core.ReadModel.Queries
{
    public class FindExistingUsernameQuery : IFindExistingUsername
    {
        private readonly IUnitOfWork _context;

        public FindExistingUsernameQuery(IUnitOfWork context)
        {
            _context = context;
        }

        /// <summary>
        /// Is the given username already registered?
        /// </summary>
        public bool Exists(string username)
        {
            return _context.Connection.Query<string>(
                "select Username from RegisteredUsernames where Username = @Username",
                new { Username = username.Trim().ToLower() }, _context.Transaction)
                .Any();
        }
    }
}