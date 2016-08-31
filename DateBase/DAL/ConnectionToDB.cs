using DataBase.Models.Crawls;
using DataBase.Models.Visitors;
using DataBase.Models.Projects;
using MongoDB.Driver;

namespace DataBase.DAL
{
    public class ConnectionToDB
    {

        public IMongoCollection<Crawl> CollectionCrawls { get; set; }
        public IMongoCollection<Visitor> CollectionVisitors { get; set; }
        public IMongoCollection<Project> CollectionProjects { get; set; }

        public ConnectionToDB()
        {
            CollectionCrawls = Connection().GetCollection<Crawl>("crawls");
            CollectionVisitors = Connection().GetCollection<Visitor>("visitors");
            CollectionProjects = Connection().GetCollection<Project>("projects");
        }
        public IMongoDatabase Connection()
        {
            return new MongoClient().GetDatabase("populator");
        }
    }
}