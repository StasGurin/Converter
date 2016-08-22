using DataBase.Models.Crawls;
using DataBase.Models.Visitors;
using MongoDB.Driver;

namespace DataBase.DAL
{
    public class ConnectionToDB
    {

        public IMongoCollection<Crawl> CollectionConvertCrawls { get; set; }
        public IMongoCollection<Visitor> CollectionConvertVisitors { get; set; }

        public ConnectionToDB()
        {
            CollectionConvertCrawls = Connection().GetCollection<Crawl>("crawls");
            CollectionConvertVisitors = Connection().GetCollection<Visitor>("visitors");
        }
        public IMongoDatabase Connection()
        {
            return new MongoClient().GetDatabase("populator");
        }
    }
}