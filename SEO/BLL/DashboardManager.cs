using DataBase.DAL;
using DataBase.Models.Visitors;
using MongoDB.Driver;
using SEO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SEO.BLL
{
    public class DashboardManager
    {
        #region Members

        private Dictionary<string, VisitStatistic> datesAndCountDictionary = new Dictionary<string, VisitStatistic>();
        private readonly DatabaseContext dataBase = new DatabaseContext();

        #endregion

        public async Task<Dictionary<string, VisitStatistic>> VisitStatCount(DateTime startDate, DateTime finishDate)
        {
            for (DateTime date = startDate; date <= finishDate; date = date.AddDays(1))
            {
                var result = await GetVisitirsByDate(date);
                int usersLinq = result.Count();
                int crawlsLinq = (from visitor in result where visitor.UserInfo.type == UserInfo.Type.crawl select visitor).Count();
                int adminsLinq = 0;
                int botsLinq = (from visitor in result where visitor.IsBot == true select visitor).Count();
                int forbiddenLinq = (from visitor in result where visitor.IsForbidden == true select visitor).Count();
                var count = new VisitStatistic(usersLinq, crawlsLinq, adminsLinq, botsLinq, forbiddenLinq);
                if (usersLinq != 0 || crawlsLinq != 0 || botsLinq != 0 || forbiddenLinq != 0 || adminsLinq != 0) datesAndCountDictionary.Add(date.ToShortDateString(), count);
            }
            return datesAndCountDictionary;
        }

        public async Task<List<VisitorDashboard>> VisitorsList(DateTime date)
        {
            var result = await GetVisitirsByDate(date);
            var visitorList = new List<VisitorDashboard>();
            visitorList.AddRange(result.Select(docVisitor => new VisitorDashboard(null, null, docVisitor.UserInfo.UserName, docVisitor.IPAddress, docVisitor.DNS,
                docVisitor.Visits.Count, docVisitor.Visits.FirstOrDefault().VisitDateTime.ToLocalTime(), docVisitor.Visits.Last().VisitDateTime.ToLocalTime().Subtract(docVisitor.Visits.FirstOrDefault().VisitDateTime.ToLocalTime()),
                docVisitor.Visits.FirstOrDefault().RefererPage, docVisitor.UserInfo.BrowserType, docVisitor.UserInfo.Platform)));
            return visitorList;
        }

        public async Task<IEnumerable<VisitDashboard>> VisitsList(DateTime date, string userName, string ip)
        {
            if (userName == null) userName = "";
            var list = new List<VisitDashboard>();
            var builderUser = Builders<Visitor>.Filter;
            var filter = builderUser.Eq(x => x.VisitDate, date) & builderUser.Eq(x => x.UserInfo.UserName, userName) & builderUser.Eq(x => x.IPAddress, ip);
            var result = await dataBase.Visitors.Find(filter).FirstOrDefaultAsync();
            var visitList = from visit in result.Visits select new VisitDashboard((DateTime)visit.VisitDateTime, visit.VisitPages.FirstOrDefault().Url, visit.RefererPage);
            return visitList;
        }


        public async Task<List<Visitor>> GetVisitirsByDate(DateTime date)
        {
            var builderUser = Builders<Visitor>.Filter;
            var filter = builderUser.Eq(x => x.VisitDate, date);
            return await dataBase.Visitors.Find(filter).ToListAsync();
        }
    }
}