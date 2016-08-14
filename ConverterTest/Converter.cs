using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.Models.Crawls;
using DataBase.Models.Visitors;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ConverterTest.Models.Crawls;
using ConverterTest.Models.Visitors;
using DataBase.DAL;

namespace ConverterTest
{
    [TestClass]
    public class Converter
    {
        ConnectionToDB dataBase = new ConnectionToDB();
        CollectionConnect sourceCollectionCon = new CollectionConnect();
       
        [TestMethod]
        public async Task ConvertCrawls()
        {

            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };

            #region crawls
            var builderIP = Builders<SourceIP>.Filter;
            var builderMasks = Builders<SourceDomainMask>.Filter;

            using (var cursorCrawls = await sourceCollectionCon.CollectionCrawls.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorCrawls.MoveNextAsync())
                {
                    var dateCrawls = cursorCrawls.Current;

                    foreach (var docCrawl in dateCrawls)
                    {


                        var convertCrawl = new Crawl
                        {
                            Id = docCrawl.Id,
                            DomainMasks = new List<string>(),
                            CrawlIP = new List<ConvertIP>(),
                            IPListUrl = docCrawl.IPListUrl,
                            Name = docCrawl.Name
                        };

                        #region masks->crawls
                        var filterMasks = builderMasks.Eq(x => x.OwnerID, docCrawl.UniqueID);
                        var resultMasks = await sourceCollectionCon.CollectionMasks.Find(filterMasks).ToListAsync();
                        foreach (var docMask in resultMasks)
                        {
                            convertCrawl.DomainMasks.Add(docMask.Name);
                        }
                        #endregion

                        #region ips->crawls
                        var filterIPS = builderIP.Eq(x => x.OwnerID, docCrawl.UniqueID);
                        var resultIPS = await sourceCollectionCon.CollectionIPS.Find(filterIPS).ToListAsync();
                        foreach (var docIPS in resultIPS)
                        {
                            var convertIPS = new ConvertIP
                            {
                                IPAddress = docIPS.IPAddress,
                                IPType = docIPS.IPType
                            };
                            convertCrawl.CrawlIP.Add(convertIPS);

                        }
                        #endregion

                        await dataBase.CollectionConvertCrawls.InsertOneAsync(convertCrawl);
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

            using (var cursorVisitors = await sourceCollectionCon.CollectionVisitors.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorVisitors.MoveNextAsync())
                {
                    var dateVisitors = cursorVisitors.Current;

                    foreach (var docVisitor in dateVisitors)
                    {
                        if (count >= 1000) break;

                        //conver visitors

                        var convertVisitor = new ConvertVisitor
                        {
                            Id = docVisitor.Id,
                            VisitDate = docVisitor.VisitDate,
                            IPAddress = docVisitor.IPAddress,
                            DNS = docVisitor.DNS,
                            Visits = new List<ConvertVisit>(),
                            UserInfo = new UserInfo()
                        };
                        
                        var filterCrawl = builderCrawl.Eq(x => x.UniqueID, docVisitor.CrawlID);
                        var resultCrawl = await sourceCollectionCon.CollectionCrawls.Find(filterCrawl).ToListAsync();

                        foreach (var docCrawl in resultCrawl)
                        {
                            if (docVisitor.CrawlID == -1) convertVisitor.CrawlID = null;
                            else convertVisitor.CrawlID = docCrawl.Id;
                        }

                        if (docVisitor.IsBot == "Nein") convertVisitor.IsBot = false;
                        if (docVisitor.IsBot == "Ja") convertVisitor.IsBot = true;
                        if (docVisitor.IsForbidden == "Nein") convertVisitor.IsForbidden = false;
                        if (docVisitor.IsForbidden == "Ja") convertVisitor.IsForbidden = true;
                        Char delimiter = ';';
                        String[] splitUserInfo = docVisitor.UserInfo.Split(delimiter);
                        convertVisitor.UserInfo.Platform = splitUserInfo[0];
                        convertVisitor.UserInfo.BrowserType = splitUserInfo[1];
                        if (splitUserInfo[2] == "True") convertVisitor.UserInfo.IsAuthenticated = true;
                        if (splitUserInfo[2] == "False") convertVisitor.UserInfo.IsAuthenticated = false;
                        convertVisitor.UserInfo.UserName = splitUserInfo[3];
                        convertVisitor.UserInfo.type = splitUserInfo[4];

                        #region visits->visitors

                        var filterVisits = builderVisits.Eq(x => x.OwnerID, docVisitor.UniqueID);
                        var resultVisits = await sourceCollectionCon.CollectionVisits.Find(filterVisits).ToListAsync();
                        foreach (var docVisit in resultVisits)
                        {
                            var convertVisits = new ConvertVisit
                            {
                                VisitDateTime = docVisit.VisitDateTime,
                                VisitPages = new List<ConvertVisitPage>(),
                                RefererPages = new List<ConvertVisitPage>()
                            };

                            #region visitPages->visits
                            var filterVisitPages = builderVisitPages.Eq(x => x.UniqueID, docVisit.PageID);
                            var resultVisitPages = await sourceCollectionCon.CollectionPages.Find(filterVisitPages).ToListAsync();
                            foreach (var docVisitPages in resultVisitPages)
                            {
                                var convertVisitPages = new ConvertVisitPage
                                {
                                    Hash = docVisitPages.Hash,
                                    Url = docVisitPages.Url
                                };
                                convertVisits.VisitPages.Add(convertVisitPages);
                            }
                            #endregion

                            #region refererPages->visits
                            var filterRefererPages = builderVisitPages.Eq(x => x.UniqueID, docVisit.RefererPageID);
                            var resultRefererPages = await sourceCollectionCon.CollectionPages.Find(filterRefererPages).ToListAsync();
                            foreach (var docRefererPages in resultRefererPages)
                            {
                                var convertRefererPages = new ConvertVisitPage
                                {
                                    Hash = docRefererPages.Hash,
                                    Url = docRefererPages.Url
                                };
                                convertVisits.RefererPages.Add(convertRefererPages);
                            }
                            #endregion

                            convertVisitor.Visits.Add(convertVisits);
                        }
                        #endregion

                        await dataBase.CollectionConvertVisitors.InsertOneAsync(convertVisitor);

                        count++;
                    }
                }
            }
            #endregion


        }
    }
}
