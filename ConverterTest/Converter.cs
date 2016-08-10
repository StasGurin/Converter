using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBase.BLL;
using DataBase.Models.Crawls;
using DataBase.Models.Visitors;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ConverterTest
{
    [TestClass]
    public class Converter
    {
        ConnectionToDB dataBase = new ConnectionToDB();

        [TestMethod]
        public async Task CovertCrawls()
        {

            var filter = new BsonDocument();
            var findOptions = new FindOptions { NoCursorTimeout = true };

            #region sp_crawls
            var builderCrawls = Builders<SourceCrawl>.Filter;
            var updateCrawls = Builders<SourceCrawl>.Update;

            using (var cursorCrawls = await dataBase.CollectionCrawls.Find(filter, findOptions).ToCursorAsync())
            {
                while (await cursorCrawls.MoveNextAsync())
                {
                    var dateCrawls = cursorCrawls.Current;

                    foreach (SourceCrawl docCrawl in dateCrawls)
                    {

                        var filterCrawls = builderCrawls.Eq(x => x.UniqueID, docCrawl.UniqueID);
                        ConvertCrawl convertCrawl = new ConvertCrawl();
                        convertCrawl.Id = docCrawl.Id;
                        convertCrawl.sp_crawls_domain_masks = new List<ConvertDomainMask>();
                        convertCrawl.sp_crawls_ips = new List<ConvertIPS>();
                        convertCrawl.IPListUrl = docCrawl.IPListUrl;
                        convertCrawl.Name = docCrawl.Name;

                        //masks->crawls
                        var builderMasks = Builders<SourceDomainMask>.Filter;
                        var filterMasks = builderMasks.Eq(x => x.OwnerID, docCrawl.UniqueID);
                        var resultMasks = await dataBase.CollectionMasks.Find(filterMasks).ToListAsync();
                        foreach (SourceDomainMask docMask in resultMasks)
                        {
                            ConvertDomainMask convertMask = new ConvertDomainMask();
                            convertMask.Id = docMask.Id;
                            convertMask.Name = docMask.Name;
                            convertCrawl.sp_crawls_domain_masks.Add(convertMask);

                        }

                        //ips->crawls
                        var builderIPS = Builders<SourceIPS>.Filter;
                        var filterIPS = builderIPS.Eq(x => x.OwnerID, docCrawl.UniqueID);
                        var resultIPS = await dataBase.CollectionIPS.Find(filterIPS).ToListAsync();
                        foreach (SourceIPS docIPS in resultIPS)
                        {
                            ConvertIPS convertIPS = new ConvertIPS();
                            convertIPS.Id = docIPS.Id;
                            convertIPS.IPAddress = docIPS.IPAddress;
                            convertIPS.IPType = docIPS.IPType;
                            convertCrawl.sp_crawls_ips.Add(convertIPS);

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
            var builderVisitors = Builders<Visitor>.Filter;
            var updateVisitors = Builders<Visitor>.Update;

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

                                    //visits->visitors
                                    var builderVisits = Builders<Visit>.Filter;
                                    var filterVisits = builderVisits.Eq("OwnerID", docVisitor.UniqueID);
                                    var resultVisits = await CollectionVisits.Find(filterVisits).ToListAsync();
                                    var updateVisitorsVisits = updateVisitors.Set("sp_visits", resultVisits);
                                    await CollectionVisitors.UpdateOneAsync(filterVisitors, updateVisitorsVisits);

                                    count++;
                                }
                            }
                        }
            count = 0;
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
