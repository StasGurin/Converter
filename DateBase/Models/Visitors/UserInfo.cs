using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DataBase.Models.Visitors
{
    public class UserInfo
    {
        #region Properties

        public string Platform { get; set; }
        public string BrowserType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        [BsonRepresentation(BsonType.String)]
        public Type type { get; set; }
        public enum Type { user, crawl };

        #endregion

        #region Constructors

        public UserInfo()
        {
        }

        public UserInfo(string platform, string browserType, bool isAuthenticated, string userName)
        {
            Platform = platform;
            BrowserType = browserType;
            IsAuthenticated = isAuthenticated;
            UserName = userName;
        }

        #endregion

    }
}