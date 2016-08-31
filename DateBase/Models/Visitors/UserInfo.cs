using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBase.Models.Visitors
{
    public class UserInfo
    {
        public string Platform { get; set; }
        public string BrowserType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Type type { get; set; }
        public enum Type { user, crawl };
      
    }
}