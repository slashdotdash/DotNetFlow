using System.Linq;
using System.Web.Mvc;
using DotNetFlow.Core.Services;

namespace DotNetFlow.Controllers
{
    public class PublishedItemsController : Controller
    {
        private readonly IPublishedItemService _publishedItems;

        public PublishedItemsController(IPublishedItemService publishedItems)
        {
            _publishedItems = publishedItems;
        }

        //
        // GET: /

        public ActionResult Index()
        {
            var itemsByDate = _publishedItems.LatestPublishedItems()
                .OrderByDescending(item => item.PublishedAt)
                // Group items by day published
                .GroupBy(item => item.PublishedAt.Date);

            return View(itemsByDate);
        }

        //
        // GET: /items/title-of-published-item

        public ActionResult Show(string slug)
        {
            var item = _publishedItems.GetItemBySlug(slug);
            return View(item);
        }
    }
}