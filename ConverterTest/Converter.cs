﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Models.Crawls;
using DataBase.Models.Visitors;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using ConverterTest.Models.Crawls;
using ConverterTest.Models.Visitors;
using DataBase.DAL;
using System.Linq;
using ConverterTest.Models.Projects;
using DataBase.Models.Projects;

namespace ConverterTest
{
    [TestClass]
    public class Converter
    {
        #region Members

        private readonly DatabaseContext dataBase = new DatabaseContext();
        private readonly CollectionConnect sourceCollectionCon = new CollectionConnect();

        #endregion

        [TestMethod]
        public async Task ConvertCrawls()
        {
            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };

            #region crawls
            var builderIP = Builders<SourceIP>.Filter;
            var builderMasks = Builders<SourceDomainMask>.Filter;

            using (var cursorCrawls = await sourceCollectionCon.SourceCrawls.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorCrawls.MoveNextAsync())
                {
                    var dateCrawls = cursorCrawls.Current;

                    foreach (var docCrawl in dateCrawls)
                    {
                        var convertCrawl = new Crawl(docCrawl.Id, docCrawl.Name, docCrawl.IPListUrl);

                        #region masks->crawls
                        var filterMasks = builderMasks.Eq(x => x.OwnerID, docCrawl.UniqueID);
                        var resultMasks = await sourceCollectionCon.SourceMasks.Find(filterMasks).ToListAsync();
                        convertCrawl.DomainMasks.AddRange(resultMasks.Select(docMask => docMask.Name));
                        #endregion

                        #region ips->crawls
                        var filterIPS = builderIP.Eq(x => x.OwnerID, docCrawl.UniqueID);
                        var resultIPS = await sourceCollectionCon.SourceIPS.Find(filterIPS).ToListAsync();
                        convertCrawl.IPs.AddRange(resultIPS.Select(docIPS => new IP(docIPS.IPType, docIPS.IPAddress)));
                        #endregion

                        await dataBase.Crawls.InsertOneAsync(convertCrawl);
                    }
                }
            }
            #endregion

        }

        [TestMethod]
        public async Task ConvertVisitors()
        {
            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };

            #region visitors
            int count = 0;
            var builderCrawl = Builders<SourceCrawl>.Filter;
            var builderVisits = Builders<SourceVisit>.Filter;
            var builderVisitPages = Builders<SourceVisitPage>.Filter;

            using (var cursorVisitors = await sourceCollectionCon.SourceVisitors.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorVisitors.MoveNextAsync())
                {
                    var dateVisitors = cursorVisitors.Current;

                    foreach (var docVisitor in dateVisitors)
                    {
                        if (count >= 1000) break;

                        //conver visitors

                        var convertVisitor = new Visitor(docVisitor.VisitDate.ToLocalTime(), docVisitor.IPAddress, docVisitor.DNS);

                        var filterCrawl = builderCrawl.Eq(x => x.UniqueID, docVisitor.CrawlID);
                        var resultCrawl = await sourceCollectionCon.SourceCrawls.Find(filterCrawl).FirstOrDefaultAsync();
                        if (resultCrawl != null)
                            convertVisitor.CrawlID = resultCrawl.Id;

                        convertVisitor.IsBot = docVisitor.IsBot == "Ja";
                        convertVisitor.IsForbidden = docVisitor.IsForbidden == "Ja";
                        var splitUserInfo = docVisitor.UserInfo.Split(';');
                        convertVisitor.UserInfo.Platform = splitUserInfo[0];
                        convertVisitor.UserInfo.BrowserType = splitUserInfo[1];
                        convertVisitor.UserInfo.IsAuthenticated = splitUserInfo[2] == "True";
                        convertVisitor.UserInfo.UserName = splitUserInfo[3];
                        convertVisitor.UserInfo.type = splitUserInfo[4] == "crawl" ? UserInfo.Type.crawl : UserInfo.Type.user;

                        #region visits->visitors

                        var filterVisits = builderVisits.Eq(x => x.OwnerID, docVisitor.UniqueID);
                        var resultVisits = await sourceCollectionCon.SourceVisits.Find(filterVisits).ToListAsync();
                        foreach (var docVisit in resultVisits)
                        {
                            var convertVisits = new Visit(docVisit.VisitDateTime);

                            #region visitPages->visits
                            var filterVisitPages = builderVisitPages.Eq(x => x.UniqueID, docVisit.PageID);
                            var resultVisitPages = await sourceCollectionCon.SourcePages.Find(filterVisitPages).ToListAsync();
                            convertVisits.VisitPages.AddRange(resultVisitPages.Select(p => new VisitPage(p.Url)));
                            #endregion

                            #region refererPages->visits
                            var filterRefererPage = builderVisitPages.Eq(x => x.UniqueID, docVisit.RefererPageID);
                            var resultRefererPage = await sourceCollectionCon.SourcePages.Find(filterRefererPage).FirstOrDefaultAsync();
                            if (resultRefererPage != null)
                                convertVisits.RefererPage = resultRefererPage.Url;
                            #endregion

                            convertVisitor.Visits.Add(convertVisits);
                        }
                        #endregion

                        await dataBase.Visitors.InsertOneAsync(convertVisitor);

                        count++;
                    }
                }
            }
            #endregion
        }

        [TestMethod]
        public async Task ConvertProjects()
        {
            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };

            #region projects
            var builderProjects = Builders<SourceProject>.Filter;
            var builderSites = Builders<SourceSite>.Filter;
            var builderSiteMapPages = Builders<SourceSiteMapPage>.Filter;

            using (var cursorProjects = await sourceCollectionCon.SourceProjects.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorProjects.MoveNextAsync())
                {
                    var dateProjects = cursorProjects.Current;

                    foreach (var docProjects in dateProjects)
                    {
                        var convertProject = new Project(docProjects.Id, docProjects.Type, docProjects.Name, docProjects.DomainName.Replace("http://",""),
                            docProjects.Keyword, docProjects.SemanticEngine, docProjects.DatabaseName, docProjects.FtpLogin,
                            docProjects.FtpServer, docProjects.FtpPassword, docProjects.FtpStartupFolder, docProjects.RobotsTXT, docProjects.SiteMapXML);
                        var splitAdmins = docProjects.AdminsNames.Split(',');
                        convertProject.AdminsNames.AddRange(splitAdmins.Select(docAdmins => docAdmins.Trim('\'')));
                        convertProject.HostingIsPayed = docProjects.HostingIsPayed == "Ja";
                        var filterSites = builderSites.Eq(x => x.Name, docProjects.DomainName.Replace("http://", ""));
                        var resultSite = await sourceCollectionCon.SourceSites.Find(filterSites).FirstOrDefaultAsync();
                        if (resultSite != null)
                        {
                            var filterSiteMapPages = builderSiteMapPages.Eq(x => x.OwnerID, resultSite.UniqueID);
                            var resultSiteMapPages = await sourceCollectionCon.SourceSiteMapPages.Find(filterSiteMapPages).ToListAsync();
                            convertProject.Pages.AddRange(resultSiteMapPages.Select(docPages => new PageInfo(docPages.Name, docPages.Type, docPages.Title, docPages.CloakedText, docPages.META_Keywords)));
                        }
                        await dataBase.Projects.InsertOneAsync(convertProject);
                    }
                }
            }
            #endregion
        }
    }
}
