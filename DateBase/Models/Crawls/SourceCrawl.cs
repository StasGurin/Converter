using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Crawls
{
    public class SourceCrawl
    {
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public string Name { get; set; }
        public string IPListUrl { get; set; }

        public List<SourceDomainMask> sp_crawls_domain_masks;

        public List<SourceIPS> sp_crawls_ips;
    }

}