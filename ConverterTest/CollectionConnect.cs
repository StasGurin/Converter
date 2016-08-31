using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConverterTest.Models.Visitors;
using ConverterTest.Models.Crawls;
using DataBase.DAL;

namespace ConverterTest
{
    class CollectionConnect
    {
        public IMongoCollection<SourceDomainMask> CollectionMasks { get; set; }
        public IMongoCollection<SourceCrawl> CollectionCrawls { get; set; }
        public IMongoCollection<SourceIP> CollectionIPS { get; set; }
        public IMongoCollection<SourceVisitPage> CollectionPages { get; set; }
        public IMongoCollection<SourceVisit> CollectionVisits { get; set; }
        public IMongoCollection<SourceVisitor> CollectionVisitors { get; set; }

        public CollectionConnect()
        {
            var connection = new DatabaseContext();
            var connectionDB = connection.Connection();

            CollectionMasks = connectionDB.GetCollection<SourceDomainMask>("sp_crawls_domain_masks");
            CollectionCrawls = connectionDB.GetCollection<SourceCrawl>("sp_crawls");
            CollectionIPS = connectionDB.GetCollection<SourceIP>("sp_crawls_ips");
            CollectionPages = connectionDB.GetCollection<SourceVisitPage>("sp_visit_pages");
            CollectionVisits = connectionDB.GetCollection<SourceVisit>("sp_visits");
            CollectionVisitors = connectionDB.GetCollection<SourceVisitor>("sp_visitors");
        }
    }
}
