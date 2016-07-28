using MongoDB.Bson;

namespace Visitors.Models
{
    public class VisitPage
    {
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public string Hash { get; set; }
        public string Url { get; set; }
    }
}