using SEO.BLL;
using SEO.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace SEO.Controllers
{

    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {
        #region Members

        DashboardManager manager = new DashboardManager();

        #endregion


        [Route]
        [HttpPost]
        public async Task<Dictionary<string, VisitStatistic>> DateStatPost(DateTime startDate, DateTime finishDate)
        {
            return await manager.VisitStatCount(startDate, finishDate);

        }

        [Route]
        [HttpPost]
        public async Task<List<VisitorDashboard>> VisitorsPost(DateTime date)
        {
            return await manager.VisitorsList(date);
        }

        [Route]
        [HttpPost]
        public async Task<IEnumerable<VisitDashboard>> VisitsPost(DateTime date, string userName, string ip)
        {
            return await manager.VisitsList(date, userName, ip);
        }
    }
}
