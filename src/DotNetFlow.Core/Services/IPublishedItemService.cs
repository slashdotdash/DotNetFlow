using System.Collections.Generic;
using DotNetFlow.Core.ReadModel.Models;

namespace DotNetFlow.Core.Services
{
    public interface IPublishedItemService
    {
        IEnumerable<PublishedItem> LatestPublishedItems();
        PublishedItem GetItemBySlug(string urlSlug);
    }
}