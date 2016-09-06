using DataBase.DAL;
using DataBase.Models.Crawls;
using MongoDB.Bson;
using MongoDB.Driver;
using SEO.Models;
using System.Threading.Tasks;

namespace SEO.BLL
{
    public class CrawlManager
    {

        private readonly DatabaseContext dataBase = new DatabaseContext();

        public async Task<CrawlInfo> CrawlsManager(VisitInfo visitInfo)
        {
            var builderCrawl = Builders<Crawl>.Filter;
            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };
            var crawlInfo = new CrawlInfo(null, false);

            using (var cursorCrawls = await dataBase.Crawls.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorCrawls.MoveNextAsync())
                {
                    var dateCrawls = cursorCrawls.Current;

                    foreach (var docCrawl in dateCrawls)
                    {
                        foreach (var docIps in docCrawl.IPs)
                        {
                            if (visitInfo.IPAddress.StartsWith(docIps.Address))
                            {
                                crawlInfo.IsCrawl = true;
                                crawlInfo.Id = docCrawl.Id;
                            }
                        }
                    }
                }
            }

            return crawlInfo;
        }

        public async Task<ResponsModel> Respons(VisitInfo visitInfo)
        {
            var crawlInfo = await CrawlsManager(visitInfo);
            var responsModel = new ResponsModel(null, null, null, crawlInfo.IsCrawl, false);
            return responsModel;
        }
    }
}