using System.Threading.Tasks;
using System.Web.Http;
using SEO.Models;
using SEO.BLL;

namespace SEO.Controllers
{
    [RoutePrefix("api/visit")]
    public class VisitController : ApiController
    {
        VisitorManager visit = new VisitorManager();

        [Route]
        [HttpPost]
        public async Task Post([FromBody]VisitInfo visitInfo)
        {
            await visit.VisitorManger(visitInfo);
        }

        //[Route("{id}")]
        //[HttpDelete]
        //public async Task Delete(string id)
        //{

        //}
    }
}
