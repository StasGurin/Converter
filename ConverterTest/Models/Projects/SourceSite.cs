using MongoDB.Bson;

namespace ConverterTest.Models.Projects
{
    class SourceSite
    {
        #region Properties
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public string Name { get; set; }
        public int LinksCount { get; set; }
        public string Type { get; set; }
        public int LatestRateID { get; set; }
        public string TitleConstantPart { get; set; }
        public string KeywordsConstantPart { get; set; }
        public string DescriptionConstantPart { get; set; }

        #endregion
    }
}
