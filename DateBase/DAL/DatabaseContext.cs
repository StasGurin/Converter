using DataBase.Models.Crawls;
using DataBase.Models.Visitors;
using DataBase.Models.Projects;
using MongoDB.Driver;

namespace DataBase.DAL
{
    public class DatabaseContext
    {

        public IMongoCollection<Crawl> Crawls { get; set; }
        public IMongoCollection<Visitor> Visitors { get; set; }
        public IMongoCollection<Project> Projects { get; set; }

        public DatabaseContext()
        {
            Crawls = Connection().GetCollection<Crawl>("crawls");
            Visitors = Connection().GetCollection<Visitor>("visitors");
            Projects = Connection().GetCollection<Project>("projects");
        }
        public IMongoDatabase Connection()
        {
            return new MongoClient().GetDatabase("populator");
        }
    }
}