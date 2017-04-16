using SEO.BLL;
using SEO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using System.Web.Http.Cors;

namespace SEO.Controllers
{

    [RoutePrefix("api/dashboard")]
    [EnableCors("http://localhost:4200", // Origin
            "*",                     // Request headers
            "*",                    // HTTP methods
            "*",                    // Response headers
            SupportsCredentials = true  // Allow credentials
        )]
    public class DashboardController : ApiController
    {
        #region Members

        DashboardManager manager = new DashboardManager();

        #endregion


        [Route]
        [HttpPost]
        public async Task<List<VisitStatistic>> DateStatPost([FromBody]DateStatModel dateStatModel )
        {
            return await manager.VisitStatCount(dateStatModel.StartDate, dateStatModel.FinishDate, dateStatModel.DomainName);

        }

        [Route]
        [HttpPost]
        public async Task<List<VisitorDashboard>> VisitorsPost(DateTime date, string domainName, string status)
        {
            return await manager.VisitorsList(date, domainName, status);
        }

        [Route]
        [HttpPost]
        public async Task<IEnumerable<VisitDashboard>> VisitsPost(DateTime date, string userName, string ip)
        {
            return await manager.VisitsList(date, userName, ip);
        }
    }
}
