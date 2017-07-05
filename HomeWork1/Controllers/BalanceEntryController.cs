using System.Web.Mvc;
using HomeWork1.Repositories;
using HomeWork1.Services;
using HomeWork1.ViewModels;

namespace HomeWork1.Controllers
{
    public class BalanceEntryController : Controller
    {
        protected AccountBookService _actBkSvr = new AccountBookService(new EFUnitOfWork());

        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(BalanceEntry entry)
        {
            if (ModelState.IsValid)
            {
                _actBkSvr.Add(entry);
                _actBkSvr.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult AjaxCreate(BalanceEntry entry)
        {
            if (ModelState.IsValid)
            {
                _actBkSvr.Add(entry);
                _actBkSvr.Save();
            }

            return View("List", _actBkSvr.Lookup());
        }

        public ActionResult List()
        {
            return View(_actBkSvr.Lookup());
        }
    }
}