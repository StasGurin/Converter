using System.Threading.Tasks;
using System.Web.Http;
using SEO.Models;
using SEO.BLL;

namespace SEO.Controllers
{
    [RoutePrefix("api/visit")]
    public class VisitController : ApiController
    {
        VisitorManager visitor = new VisitorManager();
        CrawlManager crawl = new CrawlManager();

        [Route]
        [HttpPost]
        public async Task<ResponsModel> Post([FromBody]VisitInfo visitInfo)
        {
            await visitor.VisitorsManager(visitInfo);
            return await crawl.CrawlsManager(visitInfo);
        }

        //[Route("{id}")]
        //[HttpDelete]
        //public async Task Delete(string id)
        //{

        //}
    }
}
