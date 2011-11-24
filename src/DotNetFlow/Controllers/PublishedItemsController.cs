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
        // GET: /Items/

        public ActionResult Index()
        {
            var itemsByDate = _latestPublishedItems.Execute()
                .OrderBy(item => item.PublishedAt)
                // Group items by day published
                .GroupBy(item => item.PublishedAt.Date);

            return View(itemsByDate);
        }

        //
        // GET: /Items/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }
    }
}