using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SEO.Models;
using DataBase.Models.Visitors;
using DataBase.Models.Projects;
using DataBase.DAL;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SEO.Controllers
{
    public class ValidVisitController : ApiController
    {

        private readonly ConnectionToDB dataBase = new ConnectionToDB();
        // GET: api/ValidVisit
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ValidVisit/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ValidVisit
        public async Task Post([FromBody]VisitInfo visitInfo)
        {

            var builderUser = Builders<Visitor>.Filter;
            var filterUser = builderUser.Eq(x => x.UserInfo.UserName, visitInfo.UserName) & builderUser.Eq(x => x.IPAddress, visitInfo.IPAddress) & builderUser.Eq(x => x.VisitDate, visitInfo.VisitDate);
            var resultUser = await dataBase.CollectionVisitors.Find(filterUser).FirstOrDefaultAsync();

            var page = new VisitPage(visitInfo.Url);
            var visit = new Visit(DateTime.Now)
            {
                RefererPage = visitInfo.Referer
            };
            visit.VisitPages.Add(page);

            if (resultUser == null)
            {
                var userInfo = new UserInfo
                {
                    BrowserType = visitInfo.BrowserType,
                    Platform = visitInfo.Platform,
                    UserName = visitInfo.UserName,
                    IsAuthenticated = visitInfo.isAuthenticated,
                    type = UserInfo.Type.user
                };
                var visitor = new Visitor(visitInfo.VisitDate, visitInfo.IPAddress, null)
                {
                    Id = ObjectId.GenerateNewId(),
                    UserInfo = userInfo
                };
                visitor.Visits.Add(visit);
                await dataBase.CollectionVisitors.InsertOneAsync(visitor);
            }
            else
            {
                var update = Builders<Visitor>.Update.Push(x => x.Visits, visit);
                await dataBase.CollectionVisitors.UpdateOneAsync(filterUser, update);
            }
        }

        // PUT: api/ValidVisit/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ValidVisit/5
        public void Delete(int id)
        {
        }
    }
}
