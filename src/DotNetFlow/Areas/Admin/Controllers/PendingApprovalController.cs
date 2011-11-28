using System.Linq;
using System.Web.Mvc;
using DotNetFlow.Core.Authorization;
using DotNetFlow.Core.ReadModel.Models;
using DotNetFlow.Core.ReadModel.Repositories;

namespace DotNetFlow.Areas.Admin.Controllers
{
    [RequiresPermission(Users = "ben,approver")]
    public class PendingApprovalController : Controller
    {
        private readonly IReadModelRepository<Submission> _readModelRepository;

        public PendingApprovalController(IReadModelRepository<Submission> readModelRepository)
        {            
            _readModelRepository = readModelRepository;
        }

        //
        // GET: /admin/submissions

        public ActionResult Index()
        {
            var submissions = _readModelRepository.All().OrderBy(s => s.SubmittedAt);
            return View(submissions);
        }

        //
        // GET: /Admin/Publishing/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Admin/Publishing/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Admin/Publishing/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}