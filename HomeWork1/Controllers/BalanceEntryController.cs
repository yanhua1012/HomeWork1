using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HomeWork1.Models;

namespace HomeWork1.Controllers
{
    public class BalanceEntryController : Controller
    {
        public static List<BalanceEntry> FakeBalancies = new List<BalanceEntry>(MakeFakeData(200));

        public static IEnumerable<BalanceEntry> MakeFakeData(int count)
        {
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int randNum = rand.Next(0, 10000);
                var category = randNum % 2 == 1 ? EnumCategory.Expense : EnumCategory.Revenue;
                var entry = new BalanceEntry
                {
                    Category = category,
                    Date = DateTime.Now.Subtract(new TimeSpan(0, 0, randNum)),
                    Description = $"buy {i} item at {DateTime.Now:yyyy/MM/dd HH:mm:ss}",
                    Money = Convert.ToDecimal(randNum),
                };

                yield return entry;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(BalanceEntry entry)
        {
            FakeBalancies.Add(entry);
            return RedirectToAction("Index");
        }

        public ActionResult List()
        {
            return View(FakeBalancies);
        }
    }
}