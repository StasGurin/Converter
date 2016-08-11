using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.BLL;
using DataBase.Models.Crawls;
using DataBase.Models.Visitors;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using ConverterTest.Models.Crawls;
using ConverterTest.Models.Visitors;

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

            #region sp_crawls
            var builderCrawls = Builders<SourceCrawl>.Filter;
            var updateCrawls = Builders<SourceCrawl>.Update;

            using (var cursorCrawls = await sourceCollectionCon.CollectionCrawls.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorCrawls.MoveNextAsync())
                {
                    var dateCrawls = cursorCrawls.Current;

                    foreach (SourceCrawl docCrawl in dateCrawls)
                    {

                        var filterCrawls = builderCrawls.Eq(x => x.UniqueID, docCrawl.UniqueID);
                        Crawl convertCrawl = new Crawl();
                        convertCrawl.Id = docCrawl.Id;
                        convertCrawl.DomainMasks= new List<string>();
                        convertCrawl.CrawlIP = new List<ConvertIPS>();
                        convertCrawl.IPListUrl = docCrawl.IPListUrl;
                        convertCrawl.Name = docCrawl.Name;

                        //masks->crawls
                        var builderMasks = Builders<SourceDomainMask>.Filter;
                        var filterMasks = builderMasks.Eq(x => x.OwnerID, docCrawl.UniqueID);
                        var resultMasks = await sourceCollectionCon.CollectionMasks.Find(filterMasks).ToListAsync();
                        foreach (SourceDomainMask docMask in resultMasks)
                        {
                             convertCrawl.DomainMasks.Add(docMask.Name);
                            }

                        //ips->crawls
                        var builderIPS = Builders<SourceIPS>.Filter;
                        var filterIPS = builderIPS.Eq(x => x.OwnerID, docCrawl.UniqueID);
                        var resultIPS = await sourceCollectionCon.CollectionIPS.Find(filterIPS).ToListAsync();
                        foreach (SourceIPS docIPS in resultIPS)
                        {
                            ConvertIPS convertIPS = new ConvertIPS();
                            convertIPS.IPAddress = docIPS.IPAddress;
                            convertIPS.IPType = docIPS.IPType;
                            convertCrawl.CrawlIP.Add(convertIPS);

                        }
                        await dataBase.CollectionConvertCrawls.InsertOneAsync(convertCrawl);
                    }
                }
            }
            #endregion

        }

       /* [TestMethod]
        public async Task CovertVisitors()
        {
            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };

            #region sp_visitors
            int count = 0;
            var builderVisitors = Builders<SourceVisitor>.Filter;
            var updateVisitors = Builders<SourceVisitor>.Update;

            using (var cursorVisitors = await dataBase.CollectionVisitors.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorVisitors.MoveNextAsync())
                {
                    var dateVisitors = cursorVisitors.Current;
                    if (count > 1) break;

                    foreach (SourceVisitor docVisitor in dateVisitors)
                    {
                        if (count > 1) break;
                        var filterVisitors = builderVisitors.Eq(x => x.UniqueID, docVisitor.UniqueID);
                        var convertVisitor = new ConvertVisitor();
                        convertVisitor.Id = docVisitor.Id;
                        convertVisitor.VisitDate = docVisitor.VisitDate;
                        convertVisitor.IPAddress = docVisitor.IPAddress;
                        convertVisitor.DNS = docVisitor.DNS;
                        convertVisitor.sp_visits = new List<ConvertVisit>();
                        convertVisitor.UserInfo = new UserInfo();

                        var builderCrawl = Builders<SourceCrawl>.Filter;
                        var filterCrawl = builderCrawl.Eq(x => x.UniqueID, docVisitor.CrawlID);
                        var resultCrawl = await dataBase.CollectionCrawls.Find(filterCrawl).ToListAsync();

                        foreach (SourceCrawl docCrawl in resultCrawl)
                        {
                            if (docVisitor.CrawlID == -1) convertVisitor.CrawlID = ObjectId.Empty;
                            else convertVisitor.CrawlID = docCrawl.Id;
                        }

                        if (docVisitor.IsBot == "Nein") convertVisitor.IsBot = false;
                        if (docVisitor.IsBot == "Ja") convertVisitor.IsBot = true;
                        if (docVisitor.IsForbidden == "Nein") convertVisitor.IsForbidden = false;
                        if (docVisitor.IsForbidden == "Ja") convertVisitor.IsForbidden = true;
                        Char delimiter = ';';
                        String[] substrings = docVisitor.UserInfo.Split(delimiter);
                        convertVisitor.UserInfo.Platform = substrings[0];
                        convertVisitor.UserInfo.BrowserType = substrings[1];
                        if (substrings[2] == "True") convertVisitor.UserInfo.IsAuthenticated = true;
                        if (substrings[2] == "False") convertVisitor.UserInfo.IsAuthenticated = false;
                        convertVisitor.UserInfo.UserName = substrings[3];
                        convertVisitor.UserInfo.type = substrings[4];

                        //visits->visitors
                        var builderVisits = Builders<SourceVisit>.Filter;
                        var filterVisits = builderVisits.Eq(x => x.OwnerID, docVisitor.UniqueID);
                        var resultVisits = await dataBase.CollectionVisits.Find(filterVisits).ToListAsync();
                        foreach (SourceVisit docVisit in resultVisits)
                        {
                            var convertVisits = new ConvertVisit();
                            convertVisits.Id = docVisit.Id;
                            convertVisits.VisitDateTime = docVisit.VisitDateTime;
                            convertVisits.sp_visit_pages = new List<ConvertVisitPage>();
                            convertVisits.sp_visit_referer_pages = new List<ConvertVisitPage>();
                            convertVisitor.sp_visits.Add(convertVisits);
                        }
                        await dataBase.CollectionConvertVisitors.InsertOneAsync(convertVisitor);

                        count++;
                    }
                }
            }
           /* count = 0;
            using (var cursorVisitors = await CollectionVisitors.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorVisitors.MoveNextAsync())
                {
                    var dateVisitors = cursorVisitors.Current;
                    if (count > 1000) break;

                    foreach (Visitor docVisitor in dateVisitors)
                    {
                        if (count > 1000) break;

                        var filterVisitors = builderVisitors.Eq("UniqueID", docVisitor.UniqueID);

                        //pages->visits
                        var builderPages = Builders<VisitPage>.Filter;
                        for (int i = 0; i < docVisitor.sp_visits.Count; i++)
                        {
                            var filterPages = builderPages.Eq("UniqueID", docVisitor.sp_visits[i].PageID);
                            var resultPages = await CollectionPages.Find(filterPages).ToListAsync();
                            var updateVisitorsPages = updateVisitors.Set(v => v.sp_visits[i].sp_visit_pages, resultPages);
                            await CollectionVisitors.UpdateOneAsync(filterVisitors, updateVisitorsPages);
                        }
                        count++;
                    }
                }
            }
            #endregion


        }*/
    }
}
