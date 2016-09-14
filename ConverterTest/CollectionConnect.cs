using MongoDB.Driver;
using ConverterTest.Models.Visitors;
using ConverterTest.Models.Crawls;
using DataBase.DAL;
using ConverterTest.Models.Projects;

namespace ConverterTest
{
    class CollectionConnect
    {
        public IMongoCollection<SourceDomainMask> SourceMasks { get; set; }
        public IMongoCollection<SourceCrawl> SourceCrawls { get; set; }
        public IMongoCollection<SourceIP> SourceIPS { get; set; }
        public IMongoCollection<SourceVisitPage> SourcePages { get; set; }
        public IMongoCollection<SourceVisit> SourceVisits { get; set; }
        public IMongoCollection<SourceVisitor> SourceVisitors { get; set; }
        public IMongoCollection<SourceProject> SourceProjects { get; set; }

        public CollectionConnect()
        {
            var connection = new DatabaseContext();
            var connectionDB = connection.Connection();

            SourceMasks = connectionDB.GetCollection<SourceDomainMask>("sp_crawls_domain_masks");
            SourceCrawls = connectionDB.GetCollection<SourceCrawl>("sp_crawls");
            SourceIPS = connectionDB.GetCollection<SourceIP>("sp_crawls_ips");
            SourcePages = connectionDB.GetCollection<SourceVisitPage>("sp_visit_pages");
            SourceVisits = connectionDB.GetCollection<SourceVisit>("sp_visits");
            SourceVisitors = connectionDB.GetCollection<SourceVisitor>("sp_visitors");
            SourceProjects = connectionDB.GetCollection<SourceProject>("sp_projects");
        }
    }
}
