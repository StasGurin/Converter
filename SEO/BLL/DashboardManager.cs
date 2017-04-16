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

        private List<VisitStatistic> datesAndCountDictionary = new List<VisitStatistic>();
        private readonly DatabaseContext dataBase = new DatabaseContext();

        #endregion

        public async Task<List<VisitStatistic>> VisitStatCount(DateTime startDate, DateTime finishDate, string domainName)
        {
            ////get visitors by date & dom name
            var builderUser = Builders<Visitor>.Filter;
            var filter = builderUser.Gte(x => x.VisitDate, startDate.ToUniversalTime()) & builderUser.Lte(x => x.VisitDate, finishDate.ToUniversalTime());
            var result = await dataBase.Visitors.Find(filter).ToListAsync();
            var resultByDomainName = from visit in result where visit.Visits.FirstOrDefault().VisitPages.FirstOrDefault().Url.Contains(domainName) == true select visit;
            var adminsNames = from k in PageManager.Projects where k.DomainName == domainName select k.AdminsNames;
            for (DateTime date = startDate.ToUniversalTime(); date <= finishDate.ToUniversalTime(); date = date.AddDays(1))
            {
                int usersNumber = (from visitor in resultByDomainName where visitor.UserInfo.type == UserInfo.Type.user && visitor.VisitDate == date select visitor).Count();
                int crawlsNumber = (from visitor in resultByDomainName where visitor.UserInfo.type == UserInfo.Type.crawl && visitor.VisitDate == date select visitor).Count();

                int adminsNumber = 0;
                foreach (var visit in resultByDomainName)
                {
                    foreach (var admin in adminsNames.FirstOrDefault())
                    {
                        if ((visit.UserInfo.UserName == admin) && (visit.VisitDate.ToUniversalTime() == date)) adminsNumber++;
                    }
                }

                int botsNumber = (from visitor in resultByDomainName where visitor.IsBot == true && visitor.VisitDate == date select visitor).Count();
                int forbiddenNumber = (from visitor in resultByDomainName where visitor.IsForbidden == true && visitor.VisitDate == date select visitor).Count();
                var count = new VisitStatistic(date.ToLocalTime().ToShortDateString(), usersNumber, crawlsNumber, adminsNumber, botsNumber, forbiddenNumber);
                if (usersNumber != 0 || crawlsNumber != 0 || botsNumber != 0 || forbiddenNumber != 0 || adminsNumber != 0) datesAndCountDictionary.Add(count);
            }

            return datesAndCountDictionary;
        }

        public async Task<List<VisitorDashboard>> VisitorsList(DateTime date, string domainName, string status)
        {
            var result = await GetVisitorsByDate(date, domainName, status);
            var visitorList = new List<VisitorDashboard>();
            visitorList.AddRange(result.Select(docVisitor => new VisitorDashboard(null, null, docVisitor.UserInfo.UserName, docVisitor.IPAddress, docVisitor.DNS,
                docVisitor.Visits.Count, docVisitor.Visits.FirstOrDefault().VisitDateTime.ToLocalTime(), (docVisitor.Visits.LastOrDefault().VisitDateTime.ToLocalTime().Subtract(docVisitor.Visits.FirstOrDefault().VisitDateTime.ToLocalTime())).TotalMinutes,
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


        public async Task<List<Visitor>> GetVisitorsByDate(DateTime date, string domainName, string status)
        {
            List<Visitor> visitorsList = new List<Visitor>();
            var builderUser = Builders<Visitor>.Filter;
            var filter = builderUser.Eq(x => x.VisitDate, date);
            var result = await dataBase.Visitors.Find(filter).ToListAsync();
            switch (status)
            {
                case "user":
                    {
                        visitorsList = (from visit in result where visit.Visits.FirstOrDefault().VisitPages.FirstOrDefault().Url.Contains(domainName) == true &&
                                        visit.UserInfo.type == UserInfo.Type.user select visit).ToList();
                        break;
                    }
                case "crawl":
                    {
                        visitorsList = (from visit in result where visit.Visits.FirstOrDefault().VisitPages.FirstOrDefault().Url.Contains(domainName) == true &&
                                        visit.UserInfo.type == UserInfo.Type.crawl select visit).ToList();
                        break;
                    }
                case "admin":
                    {
                        var adminsNames = from admin in PageManager.Projects where admin.DomainName == domainName select admin.AdminsNames;
                        var resultByDomainName = from visit in result where visit.Visits.FirstOrDefault().VisitPages.FirstOrDefault().Url.Contains(domainName) == true select visit;
                        foreach (var visit in resultByDomainName)
                        {
                            foreach (var admin in adminsNames.FirstOrDefault())
                            {
                                if ((visit.UserInfo.UserName == admin))
                                {
                                    visitorsList.Add(visit);
                                }
                            }
                        }

                        break;
                    }
                case "bot":
                    {
                        visitorsList = (from visit in result where visit.Visits.FirstOrDefault().VisitPages.FirstOrDefault().Url.Contains(domainName) == true &&
                                        visit.IsBot == true select visit).ToList();
                        break;
                    }
                case "forbidden":
                    {
                        visitorsList = (from visit in result where visit.Visits.FirstOrDefault().VisitPages.FirstOrDefault().Url.Contains(domainName) == true &&
                                        visit.IsForbidden == true select visit).ToList();
                        break;
                    }
                default: break;
            }

            return visitorsList;
        }
    }
}