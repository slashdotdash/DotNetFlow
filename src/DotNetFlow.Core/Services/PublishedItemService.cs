using System.Collections.Generic;
using DotNetFlow.Core.Exceptions;
using DotNetFlow.Core.Infrastructure;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Queries;

namespace DotNetFlow.Core.Services
{
    public sealed class PublishedItemService : IPublishedItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublishedItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<PublishedItem> LatestPublishedItems()
        {
            return new LatestPublishedItemsQuery(_unitOfWork).Execute();
        }

        public PublishedItem GetItemBySlug(string urlSlug)
        {
            var item = new GetPublishedItemByUrlSlugQuery(_unitOfWork).Execute(urlSlug);

            Guard.Against<PublishedItemNotFoundException>(item == null, "Published item not found for url '{0}'", urlSlug);

            return item;
        }
    }
}