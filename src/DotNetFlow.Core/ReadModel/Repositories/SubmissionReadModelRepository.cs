using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.ReadModel.Models;

namespace DotNetFlow.Core.ReadModel.Repositories
{
    public sealed class SubmissionReadModelRepository : IReadModelRepository<Submission>
    {
        private readonly IUnitOfWork _context;

        public SubmissionReadModelRepository(IUnitOfWork context)
        {
            _context = context;
        }

        public Submission Get(Guid id)
        {
            return _context.Connection.Query<Submission>("select * from Submissions where ItemId = @Id", new { Id = id }, _context.Transaction).Single();
        }

        public IEnumerable<Submission> All()
        {
            return _context.Connection.Query<Submission>("select * from Submissions", null, _context.Transaction);
        }
    }
}