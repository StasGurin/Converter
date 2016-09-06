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

        public async Task<PageRenderingInfo> CrawlsManager(VisitInfo visitInfo)
        {
            var crawlInfo = new PageRenderingInfo(null, false);

            var lenght = visitInfo.IPAddress.LastIndexOf('.');
            var startIndex = 0;

            var buildCrawl = Builders<Crawl>.Filter;
            var filterIPs = buildCrawl.ElemMatch(x => x.IPs, x => x.Address == visitInfo.IPAddress.Substring(startIndex, lenght));
            var resultCrawl = await dataBase.Crawls.Find(filterIPs).FirstOrDefaultAsync();

            if (resultCrawl != null)
            {
                crawlInfo.IsCrawl = true;
                crawlInfo.Id = resultCrawl.Id;
            }

            return crawlInfo;
        }

        public async Task<ResponseModel> Respons(VisitInfo visitInfo)
        {
            var crawlInfo = await CrawlsManager(visitInfo);
            var responsModel = new ResponseModel(null, null, null, crawlInfo.IsCrawl, false);
            return responsModel;
        }
    }
}