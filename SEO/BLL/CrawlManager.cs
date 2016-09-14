using DataBase.DAL;
using DataBase.Models.Crawls;
using MongoDB.Bson;
using MongoDB.Driver;
using SEO.Models;
using System.Collections.Generic;
using System.Linq;

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
                foreach (var docIPs in crawl.IPs)
                {
                    if (visitInfo.IPAddress.StartsWith(docIPs.Address))
                    {
                        return new PageRenderingInfo(crawl.Id, true);
                    }
                }
            }
            return new PageRenderingInfo(null, false);
        }

        public static ResponseModel InitResponse(PageRenderingInfo respons)
        {
            return new ResponseModel(null, null, null, respons.IsCrawl, false);
        }
    }
}