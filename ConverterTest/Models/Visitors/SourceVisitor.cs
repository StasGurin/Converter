using MongoDB.Bson;

namespace ConverterTest.Models.Visitors
{
    public class SourceVisitor
    {
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public BsonDateTime VisitDate { get; set; }
        public string IPAddress { get; set; }
        public int CrawlID { get; set; }
        public string DNS { get; set; }
        public string UserInfo { get; set; }
        public string UserName { get; set; }
        public string IsBot { get; set; }
        public string IsForbidden { get; set; }
    }
}