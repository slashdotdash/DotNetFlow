using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.ReadModel.Models;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _context;

        public UserRepository(IUnitOfWork context)
        {
            _context = context;
        }

        public UserAccountModel Get(Guid id)
        {
            return _context.Connection.Query<UserAccountModel>("select * from Users where UserId = @Id", new { Id = id }, _context.Transaction)
                .Single();
        }

        public IEnumerable<UserAccountModel> All()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Find an existing user by their email address, returns null if no user found
        /// </summary>
        public UserAccountModel FindByEmail(string email)
        {
            return _context.Connection.Query<UserAccountModel>("select * from Users where Email = @Email", new { Email = email }, _context.Transaction)
                .SingleOrDefault();
        }
    }
}