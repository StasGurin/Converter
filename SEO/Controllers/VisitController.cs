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


        [Route]
        [HttpPost]
        public async Task<ResponseModel> Post([FromBody]VisitInfo visitInfo)
        {
            var respons = CrawlManager.RecognitionCrawl(visitInfo);
            await visitor.VisitorsManager(visitInfo, respons);
            return CrawlManager.InitResponse(respons);
        }

        //[Route("{id}")]
        //[HttpDelete]
        //public async Task Delete(string id)
        //{

        //}
    }
}
