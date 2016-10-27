using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Crawls
{
    public class Crawl
    {
        #region Properties

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string IPListUrl { get; set; }
        public List<string> DomainMasks { get; set; }
        public List<IP> IPs { get; set; }

        #endregion

        #region Constructors

        public Crawl()
        {
        }

        public Crawl(ObjectId id, string name, string ipListUrl)
        {
            Id = id;
            Name = name;
            IPListUrl = ipListUrl;
            DomainMasks = new List<string>();
            IPs = new List<IP>();
        }

        public Crawl(ObjectId id, string name, string ipListUrl, List<string> domainMasks, List<IP> ips)
        {
            Id = id;
            Name = name;
            IPListUrl = ipListUrl;
            DomainMasks = domainMasks;
            IPs = ips;
        }

        #endregion
    }
}