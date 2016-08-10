using MongoDB.Bson;

namespace DataBase.Models.Crawls
{
    public class ConvertIPS
    {
        public ObjectId Id { get; set; }
        public string IPType { get; set; }
        public string IPAddress { get; set; }
    }
}