using System;
using System.Web.Mvc;
using HomeWork1.Repositories;
using HomeWork1.Services;
using HomeWork1.ViewModels;

namespace HomeWork1.Areas.Management.Controllers
{
    public class BalanceEntryController : Controller
    {
        protected AccountBookService _actBkSvr = new AccountBookService(new EFUnitOfWork());

        public ActionResult Edit(Guid id)
        {
            if (this.User.Identity.Name == "admin")
            {
                // 管理員可以編輯所有的歷史資料
                var model = _actBkSvr.Get(id);
                return View(model);
            }
            else
            {
                // 非管理員進入會「跳回首頁」
                return RedirectToAction("Index", "BalanceEntry", new { Area = "" });
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(BalanceEntry entry)
        {
            if (ModelState.IsValid)
            {
                _actBkSvr.Update(entry);
                _actBkSvr.Save();
                return RedirectToAction("Index", "BalanceEntry", new { Area = "" });
            }
            else
            {
                return View("Edit");
            }
        }
    }
}