using System.Web.Mvc;
using HomeWork1.Repositories;
using HomeWork1.Services;
using HomeWork1.ViewModels;
using PagedList;
using System.Linq;

namespace HomeWork1.Controllers
{
    public class BalanceEntryController : Controller
    {
        protected AccountBookService _actBkSvr = new AccountBookService(new EFUnitOfWork());

        const int PageSize = 10;

        public ActionResult Index(int? page)
        {
            int pageNumber = page.HasValue ? page.Value : 1;
            if (pageNumber < 1) { pageNumber = 1; }

            this.ViewData.Add("Page", pageNumber);
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


            return View("List", _actBkSvr.Lookup().OrderByDescending(x => x.Date).ToPagedList(1, PageSize));
        }

        public ActionResult List(int? page)
        {
            int pageNumber = page.HasValue ? page.Value : 1;
            if (pageNumber < 1) { pageNumber = 1; }

            // 一定要先做排序處理，否則分頁處理會報錯
            var model = _actBkSvr.Lookup().OrderByDescending(x => x.Date).ToPagedList(pageNumber, PageSize);
            return View(model);
        }
    }
}