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

        public async Task<ResponsModel> CrawlsManager(VisitInfo visitInfo)
        {
            var builderCrawl = Builders<Crawl>.Filter;
            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };
            var responsModel = new ResponsModel(null, null, null, false, false);

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
                                responsModel.IsCrawl = true;

                        }
                    }
                }
            }

            return responsModel;
        }
    }
}