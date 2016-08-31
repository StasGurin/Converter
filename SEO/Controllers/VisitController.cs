using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SEO.Models;
using DataBase.Models.Visitors;
using DataBase.DAL;
using SEO.BLL;
using MongoDB.Driver;

namespace SEO.Controllers
{
    public class VisitController : ApiController
    {

        private readonly DatabaseContext dataBase = new DatabaseContext();
        ValidUser visit = new ValidUser();
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
            var resultUser = await dataBase.Visitors.Find(filterUser).FirstOrDefaultAsync();

            if (resultUser == null)
                await dataBase.Visitors.InsertOneAsync(visit.CreateUser(visitInfo));
            else
                await dataBase.Visitors.UpdateOneAsync(filterUser, visit.UpdateUser(visitInfo));

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
