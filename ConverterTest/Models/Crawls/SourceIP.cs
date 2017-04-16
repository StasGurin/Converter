using MongoDB.Bson;

namespace ConverterTest.Models.Crawls
{
    public class SourceIP
    {
        #region Properties

        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public string IPType { get; set; }
        public string IPAddress { get; set; }

        #endregion
    }
}