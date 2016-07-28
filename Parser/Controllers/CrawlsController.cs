using MongoDB.Driver;
using System.Web.Http;
using Visitors.Models;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Parser.Controllers
{
    public class CrawlsController : ApiController
    {
        private IMongoCollection<DomainMask> CollectionMasks { get; set; }
        private IMongoCollection<Crawl> CollectionCrawls { get; set; }
        private IMongoCollection<IPS> CollectionIPS { get; set; }

        public CrawlsController()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("populator");
            CollectionMasks = database.GetCollection<DomainMask>("sp_crawls_domain_masks");
            CollectionCrawls = database.GetCollection<Crawl>("sp_crawls");
            CollectionIPS = database.GetCollection<IPS>("sp_crawls_ips");
        }


        // GET: api/Crawls/5
        public string Get(int id)
        {
            return "value";
        }

        // PATCH: api/Crawls
        public async Task Patch()
        {

            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };

            #region sp_crawls
            var builderCrawls = Builders<Crawl>.Filter;
            var updateCrawls = Builders<Crawl>.Update;

            using (var cursorCrawls = await CollectionCrawls.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorCrawls.MoveNextAsync())
                {
                    var dateCrawls = cursorCrawls.Current;

                    foreach (Crawl docCrawl in dateCrawls)
                    {

                        var filterCrawls = builderCrawls.Eq("UniqueID", docCrawl.UniqueID);

                        //masks->crawls
                        var builderMasks = Builders<DomainMask>.Filter;
                        var filterMasks = builderMasks.Eq("OwnerID", docCrawl.UniqueID);
                        var resultMasks = await CollectionMasks.Find(filterMasks).ToListAsync();
                        var updateCrawlsMasks = updateCrawls.Set("sp_crawls_domain_masks", resultMasks);

                        //ips->crawls
                        var builderIPS = Builders<IPS>.Filter;
                        var filterIPS = builderIPS.Eq("OwnerID", docCrawl.UniqueID);
                        var resultIPS = await CollectionIPS.Find(filterIPS).ToListAsync();
                        var updateCrawlsIPS = updateCrawls.Set("sp_crawls_ips", resultIPS);

                        await CollectionCrawls.UpdateManyAsync(filterCrawls, updateCrawlsMasks);
                        await CollectionCrawls.UpdateManyAsync(filterCrawls, updateCrawlsIPS);
                    }
                }
            }
            #endregion

        }


        // PUT: api/Crawls/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Crawls/5
        public void Delete(int id)
        {
        }
    }
}


