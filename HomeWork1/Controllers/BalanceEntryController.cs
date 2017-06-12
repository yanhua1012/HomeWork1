using System.Collections.Generic;
using System.Web.Mvc;
using HomeWork1.Models;

namespace HomeWork1.Controllers
{
    public class BalanceEntryController : Controller
    {

        public static List<BalanceEntry> FakeBalancies = new List<BalanceEntry>();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BalanceEntry entry)
        {
            FakeBalancies.Add(entry);
            //object model = null;
            // ToDo: 如何清空欄位?
            //return View("Index", model); 
            return new RedirectResult("Index");
        }

        public ActionResult List()
        {
            return View(FakeBalancies);
        }
    }
}