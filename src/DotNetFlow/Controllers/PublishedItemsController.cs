using System.Linq;
using System.Web.Mvc;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Queries;

namespace DotNetFlow.Controllers
{
    public class PublishedItemsController : Controller
    {
        private readonly IQueryModel<PublishedItem> _latestPublishedItems;

        public PublishedItemsController(IQueryModel<PublishedItem> latestPublishedItems)
        {
            _latestPublishedItems = latestPublishedItems;
        }

        //
        // GET: /

        public ActionResult Index()
        {
            var itemsByDate = _latestPublishedItems.Execute()
                .OrderByDescending(item => item.PublishedAt)
                // Group items by day published
                .GroupBy(item => item.PublishedAt.Date);

            return View(itemsByDate);
        }

        //
        // GET: /items/title-of-published-item

        public ActionResult Show(string slug)
        {
            //var item = _latestPublishedItems.GetItemBySlug(slug);
            return View();
        }
    }
}