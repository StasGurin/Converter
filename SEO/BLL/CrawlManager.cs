using DataBase.DAL;
using DataBase.Models.Crawls;
using MongoDB.Bson;
using MongoDB.Driver;
using SEO.Models;
using System.Collections.Generic;

namespace SEO.BLL
{
    public static class CrawlManager
    {

        private static readonly DatabaseContext dataBase = new DatabaseContext();

        public static List<Crawl> Crawls = new List<Crawl>();
        static CrawlManager()
        {
            var filter = new BsonDocument();
            var result = dataBase.Crawls.Find(filter).ToListAsync().Result;

            Crawls.AddRange(result);
        }

        public static PageRenderingInfo RecognitionCrawl(VisitInfo visitInfo)
        {

            foreach (var crawl in Crawls)
            {
                foreach (var IPs in crawl.IPs)
                {
                    if (visitInfo.IPAddress.StartsWith(IPs.Address))
                    {
                        var seoPageInfo = PageManager.ResponsePage(visitInfo.Url);
                        return new PageRenderingInfo(crawl.Id, true, seoPageInfo.Title, seoPageInfo.Body, seoPageInfo.Keywords);
                    }
                }
            }
            return new PageRenderingInfo(null, false, null, null, null);
        }

        public static ResponseModel InitResponse(PageRenderingInfo respons)
        {
            return new ResponseModel(respons.Title, respons.Body, respons.Keywords, respons.IsCrawl, false);
        }
    }
}