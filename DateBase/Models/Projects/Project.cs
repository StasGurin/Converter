using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Projects
{
    public class Project
    {

        #region Properties

        public ObjectId Id { get; set; }
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
        public List<string> AdminsNames { get; set; }
        public bool HostingIsPayed { get; set; }
        public List<PageInfo> Pages { get; set; }

        #endregion

        #region Constructors
        public Project()
        {
        }

        public Project(ObjectId id, string type, string name, string domainName, string keyword, string semanticEngine, string databaseName,
        string ftpLogin, string ftpServer, string ftpPassword, string ftpStartupFolder, string robotsTXT, string siteMapXML)
        {
            Id = id;
            Type = type;
            Name = name;
            DomainName = domainName;
            Keyword = keyword;
            SemanticEngine = semanticEngine;
            DatabaseName = databaseName;
            FtpLogin = ftpLogin;
            FtpServer = ftpServer;
            FtpPassword = ftpPassword;
            FtpStartupFolder = ftpStartupFolder;
            RobotsTXT = robotsTXT;
            SiteMapXML = siteMapXML;
            AdminsNames = new List<string>();
            Pages = new List<PageInfo>();
        }

        #endregion

    }
}