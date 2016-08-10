using MongoDB.Bson;

namespace DataBase.Models.Crawls
{
    public class SourceDomainMask
    {
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public string Name { get; set; }
    }
}