﻿using System.ServiceModel.Syndication;
using System.Web.Mvc;
using System.Xml;

namespace HomeWork1.CustomResults
{
    public class RssResult : ActionResult
    {
        SyndicationFeed feed;

        public RssResult(SyndicationFeed feed)
        {
            this.feed = feed;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "application/rss+xml";
            Rss20FeedFormatter formatter = new Rss20FeedFormatter(this.feed);
            using (var writer = XmlWriter.Create(context.HttpContext.Response.Output))
            {
                formatter.WriteTo(writer);
            }
        }
    }
}