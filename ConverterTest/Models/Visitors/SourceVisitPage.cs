using MongoDB.Bson;

namespace ConverterTest.Models.Visitors
{
    public class SourceVisitPage
    {
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public string Hash { get; set; }
        public string Url { get; set; }
    }
}