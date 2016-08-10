using MongoDB.Driver;
using DataBase.Models.Crawls;
using DataBase.Models.Visitors;

namespace DataBase.BLL
{
    public class ConnectionToDB
    {
        public IMongoCollection<SourceDomainMask> CollectionMasks { get; set; }
        public IMongoCollection<SourceCrawl> CollectionCrawls { get; set; }
        public IMongoCollection<SourceIPS> CollectionIPS { get; set; }
        public IMongoCollection<VisitPage> CollectionPages { get; set; }
        public IMongoCollection<Visit> CollectionVisits { get; set; }
        public IMongoCollection<Visitor> CollectionVisitors { get; set; }
        public IMongoCollection<ConvertCrawl> CollectionConvertCrawls { get; set; }

        public ConnectionToDB()
        {
            CollectionMasks = Connection().GetCollection<SourceDomainMask>("sp_crawls_domain_masks");
            CollectionCrawls = Connection().GetCollection<SourceCrawl>("sp_crawls");
            CollectionIPS = Connection().GetCollection<SourceIPS>("sp_crawls_ips");
            CollectionPages = Connection().GetCollection<VisitPage>("sp_visit_pages");
            CollectionVisits = Connection().GetCollection<Visit>("sp_visits");
            CollectionVisitors = Connection().GetCollection<Visitor>("sp_visitors");
            CollectionConvertCrawls = Connection().GetCollection<ConvertCrawl>("sp_convert_crawls");
        }
        public IMongoDatabase Connection()
        {
            var client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("populator");
            return database;
        }
    }
}