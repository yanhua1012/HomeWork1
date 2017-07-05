using System;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using HomeWork1.CustomResults;
using HomeWork1.Helper;
using HomeWork1.Repositories;
using HomeWork1.Services;

namespace HomeWork1.Controllers
{
    public class FeedController : Controller
    {
        protected AccountBookService _actBkSvr = new AccountBookService(new EFUnitOfWork());

        // GET: Feed
        public ActionResult Index()
        {
            var feed = this.GetFedData();
            return new RssResult(feed);
        }

        private SyndicationFeed GetFedData()
        {
            var feed = new SyndicationFeed("我的記帳資料", "記帳RSS!", new Uri(Url.Action("Rss", "Feed", null, "http")));

            var datas = _actBkSvr.Lookup().Where(x => x.Date <= DateTime.Now)
                .OrderBy(x => x.Date);

            feed.Items = datas.Select(x => new SyndicationItem
            (
                x.Date.ToString(), 
                $"{x.GetCategoryText()}:{x.Money}", 
                new Uri(Url.Action("Detail", "BalanceEntry", 
                new { id = x.Id }, "http")), 
                x.Id.ToString(), DateTime.Now
            ));

            return feed;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}