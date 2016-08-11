using MongoDB.Driver;
using DataBase.Models.Crawls;
using DataBase.Models.Visitors;

namespace DataBase.BLL
{
    public class ConnectionToDB
    {

        public IMongoCollection<Crawl> CollectionConvertCrawls { get; set; }
        public IMongoCollection<ConvertVisitor> CollectionConvertVisitors { get; set; }

        public ConnectionToDB()
        {
            CollectionConvertCrawls = Connection().GetCollection<Crawl>("crawls");
            CollectionConvertVisitors = Connection().GetCollection<ConvertVisitor>("visitors");
        }
        public IMongoDatabase Connection()
        {
            var client = new MongoClient();
            IMongoDatabase database = client.GetDatabase("populator");
            return database;
        }
    }
}