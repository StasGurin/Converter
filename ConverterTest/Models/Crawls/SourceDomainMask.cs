using MongoDB.Bson;

namespace ConverterTest.Models.Crawls
{
    public class SourceDomainMask
    {
        #region Properties

        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public string Name { get; set; }

        #endregion
    }
}