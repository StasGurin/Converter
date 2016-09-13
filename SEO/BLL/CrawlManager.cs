using DataBase.DAL;
using DataBase.Models.Crawls;
using MongoDB.Bson;
using MongoDB.Driver;
using SEO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            Crawls.AddRange(result.Select(crawls => new Crawl(crawls.Id, crawls.Name, crawls.IPListUrl, crawls.DomainMasks, crawls.IPs)));
        }

        public static PageRenderingInfo RecognitionCrawl(VisitInfo visitInfo)
        {

            foreach (var docCrawls in Crawls)
            {
                foreach (var docIPs in docCrawls.IPs)
                {
                    if (docIPs.Address.StartsWith(visitInfo.IPAddress))
                    {
                        return new PageRenderingInfo(docCrawls.Id, true);
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