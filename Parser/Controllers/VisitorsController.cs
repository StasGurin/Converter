using MongoDB.Driver;
using System.Web.Http;
using Visitors.Models;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Visitors.Controllers
{
    public class VisitorsController : ApiController
    {
        private IMongoCollection<VisitPage> CollectionPages { get; set; }
        private IMongoCollection<Visit> CollectionVisits { get; set; }
        private IMongoCollection<Visitor> CollectionVisitors { get; set; }

        public VisitorsController()
        {
            var client = new MongoClient();
            var database = client.GetDatabase("populator");
            CollectionPages = database.GetCollection<VisitPage>("sp_visit_pages");
            CollectionVisits = database.GetCollection<Visit>("sp_visits");
            CollectionVisitors = database.GetCollection<Visitor>("sp_visitors");
        }


        // GET: api/Visitors/5
        public string Get(int id)
        {
            return "value";
        }

        // PATCH: api/Visitors
        public async Task Patch()
        {

            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };

            #region sp_visitors
            int count = 0;
            var builderVisitors = Builders<Visitor>.Filter;
            var updateVisitors = Builders<Visitor>.Update;

            using (var cursorVisitors = await CollectionVisitors.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorVisitors.MoveNextAsync())
                {
                    var dateVisitors = cursorVisitors.Current;

                    foreach (Visitor docVisitor in dateVisitors)
                    {
                        if (count >= 1000) break;
                        var filterVisitors = builderVisitors.Eq("UniqueID", docVisitor.UniqueID);

                        //visits->visitors
                        var builderVisits = Builders<Visit>.Filter;
                        var filterVisits = builderVisits.Eq("OwnerID", docVisitor.UniqueID);
                        var resultVisits = await CollectionVisits.Find(filterVisits).ToListAsync();
                        var updateVisitorsVisits = updateVisitors.Set("sp_visits", resultVisits);
                        await CollectionVisitors.UpdateManyAsync(filterVisitors, updateVisitorsVisits);

                        //pages->visits
                        var builderPages = Builders<VisitPage>.Filter;
                        for (int i = 0; i < docVisitor.sp_visits.Count; i++)
                        {
                            var filterPages = builderPages.Eq("UniqueID", docVisitor.sp_visits[i].PageID);
                            var resultPages = await CollectionPages.Find(filterPages).ToListAsync();
                            var updateVisitorsPages = updateVisitors.Set(v => v.sp_visits[i].sp_visit_pages, resultPages);
                            await CollectionVisitors.UpdateManyAsync(filterVisitors, updateVisitorsPages);
                        }
                        count++;
                    }
                }
            }
            #endregion

        }


        // PUT: api/Visitors/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Visitors/5
        public void Delete(int id)
        {
        }
    }
}
