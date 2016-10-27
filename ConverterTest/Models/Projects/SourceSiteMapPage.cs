using MongoDB.Bson;

namespace ConverterTest.Models.Projects
{
    class SourceSiteMapPage
    {
        #region Properties
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string AddConstantTitle { get; set; }
        public string META_Keywords { get; set; }
        public string CloakedHeader { get; set; }
        public string AddConstantKeywords { get; set; }
        public string CloakedText { get; set; }
        public string META_Description { get; set; }
        public string ChangeFrequency { get; set; }
        public string AddConstantDescription { get; set; }
        public double Priority { get; set; }
        public string SiteMapIgnore { get; set; }
        public string RobotsTXTSuffix { get; set; }
        public string ParameterName { get; set; }
        public string ParameterTableName { get; set; }
        public string ParameterFieldName { get; set; }

        #endregion

        #region Constructors
        public SourceSiteMapPage()
        {
        }

        #endregion
    }
}
