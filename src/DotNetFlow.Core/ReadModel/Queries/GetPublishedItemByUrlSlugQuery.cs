using System.Linq;
using Dapper;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.ReadModel.Models;

namespace DotNetFlow.Core.ReadModel.Queries
{
    public sealed class GetPublishedItemByUrlSlugQuery
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetPublishedItemByUrlSlugQuery(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PublishedItem Execute(string urlSlug)
        {
            return _unitOfWork.Connection.Query<PublishedItem>(
                "select * from Items where UrlSlug = @UrlSlug",
                new { UrlSlug = urlSlug }, _unitOfWork.Transaction).FirstOrDefault();
        }
    }
}