using System.Collections.Generic;
using Dapper;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.ReadModel.Models;

namespace DotNetFlow.Core.ReadModel.Queries
{
    public sealed class LatestPublishedItemsQuery : IQueryModel<PublishedItem>
    {
        private readonly IUnitOfWork _unitOfWork;

        public LatestPublishedItemsQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PublishedItem> Execute()
        {
            return _unitOfWork.Connection.Query<PublishedItem>(
                "select * from Items order by PublishedAt desc",
                null, _unitOfWork.Transaction);
        }
    }
}