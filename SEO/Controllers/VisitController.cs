using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using SEO.Models;
using DataBase.DAL;
using SEO.BLL;
using MongoDB.Driver;

namespace SEO.Controllers
{
    public class VisitController : ApiController
    {

        private readonly DatabaseContext dataBase = new DatabaseContext();
        VisitorManager visit = new VisitorManager();

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
            var visitFilter = visit.resultVisitor(visitInfo);
            var resultUser = await dataBase.Visitors.Find(visitFilter).FirstOrDefaultAsync();

            if (resultUser == null)
                await dataBase.Visitors.InsertOneAsync(visit.CreateUser(visitInfo));
            else
                await dataBase.Visitors.UpdateOneAsync(visitFilter, visit.UpdateUser(visitInfo));

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
