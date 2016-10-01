﻿using SEO.Models;
using DataBase.Models.Visitors;
using DataBase.DAL;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;

namespace SEO.BLL
{
    public class VisitorManager
    {
        private readonly DatabaseContext dataBase = new DatabaseContext();

        public FilterDefinition<Visitor> resultVisitor(VisitInfo visitInfo)
        {
            var builderUser = Builders<Visitor>.Filter;
            return builderUser.Eq(x => x.UserInfo.UserName, visitInfo.UserName) & builderUser.Eq(x => x.IPAddress, visitInfo.IPAddress) & builderUser.Eq(x => x.VisitDate, visitInfo.VisitDate);
        }

        public Visitor CreateVisit(VisitInfo visitInfo, PageRenderingInfo crawlInfo)
        {

            var page = new VisitPage(visitInfo.Url);
            var visit = new Visit(DateTime.Now, visitInfo.Referer);
            visit.VisitPages.Add(page);

            var userInfo = new UserInfo(visitInfo.Platform, visitInfo.BrowserType, visitInfo.IsAuthenticated, visitInfo.UserName)
            {
                type = crawlInfo.IsCrawl == true ? UserInfo.Type.crawl : UserInfo.Type.user
            };
            var visitor = new Visitor(ObjectId.GenerateNewId(), visitInfo.VisitDate, visitInfo.IPAddress, crawlInfo.Id, null, userInfo);

            visitor.Visits.Add(visit);
            return visitor;
        }

        public UpdateDefinition<Visitor> UpdateVisit(VisitInfo visitInfo)
        {
            var page = new VisitPage(visitInfo.Url);
            var visit = new Visit(DateTime.Now, visitInfo.Referer);
            visit.VisitPages.Add(page);
            var update = Builders<Visitor>.Update.Push(x => x.Visits, visit);
            return update;
        }

        public async Task VisitorsManager(VisitInfo visitInfo, PageRenderingInfo respons)
        {
            var visitFilter = resultVisitor(visitInfo);
            var resultUser = await dataBase.Visitors.Find(visitFilter).FirstOrDefaultAsync();
            if (resultUser == null)
                await dataBase.Visitors.InsertOneAsync(CreateVisit(visitInfo, respons));
            else
                await dataBase.Visitors.UpdateOneAsync(visitFilter, UpdateVisit(visitInfo));
        }
    }
}