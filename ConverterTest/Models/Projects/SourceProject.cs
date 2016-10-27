using MongoDB.Bson;

namespace ConverterTest.Models.Projects
{
    class SourceProject
    {

        #region Properties
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string DomainName { get; set; }
        public string Keyword { get; set; }
        public string SemanticEngine { get; set; }
        public string DatabaseName { get; set; }
        public string FtpLogin { get; set; }
        public string FtpServer { get; set; }
        public string FtpPassword { get; set; }
        public string FtpStartupFolder { get; set; }
        public string RobotsTXT { get; set; }
        public string SiteMapXML { get; set; }
        public string AdminsNames { get; set; }
        public string HostingIsPayed { get; set; }
        #endregion

        #region Constructors
        public SourceProject()
        {
        }

        #endregion

    }
}
