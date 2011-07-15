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
        public UserAccountModel FindByUsernameOrEmail(string usernameOrEmail)
        {
            return _context.Connection.Query<UserAccountModel>("select * from Users where Username = @UsernameOrEmail or Email = @UsernameOrEmail", 
                new { UsernameOrEmail = usernameOrEmail }, _context.Transaction)
                .SingleOrDefault();
        }
    }
}