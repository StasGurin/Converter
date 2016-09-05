using SEO.Models;
using DataBase.Models.Visitors;
using DataBase.DAL;
using System;
using MongoDB.Driver;
using MongoDB.Bson;

namespace SEO.BLL
{
    public class VisitorManager
    {
        public FilterDefinition<Visitor> resultVisitor(VisitInfo visitInfo)
        {
            var builderUser = Builders<Visitor>.Filter;
            return builderUser.Eq(x => x.UserInfo.UserName, visitInfo.UserName) & builderUser.Eq(x => x.IPAddress, visitInfo.IPAddress) & builderUser.Eq(x => x.VisitDate, visitInfo.VisitDate);
        }

        public Visitor CreateUser(VisitInfo visitInfo)
        {

            var page = new VisitPage(visitInfo.Url);
            var visit = new Visit(DateTime.Now, visitInfo.Referer);
            visit.VisitPages.Add(page);

            var userInfo = new UserInfo(visitInfo.Platform, visitInfo.BrowserType, visitInfo.IsAuthenticated, visitInfo.UserName)
            {
                type = UserInfo.Type.user
            };
            var visitor = new Visitor(ObjectId.GenerateNewId(), visitInfo.VisitDate, visitInfo.IPAddress, null)
            {
                UserInfo = userInfo
            };
            visitor.Visits.Add(visit);
            return visitor;
        }

        public UpdateDefinition<Visitor> UpdateUser(VisitInfo visitInfo)
        {
            var page = new VisitPage(visitInfo.Url);
            var visit = new Visit(DateTime.Now, visitInfo.Referer);
            var update = Builders<Visitor>.Update.Push(x => x.Visits, visit);
            return update;
        }

    }
}