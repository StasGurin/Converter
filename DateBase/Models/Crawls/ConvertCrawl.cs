using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Crawls
{
    public class ConvertCrawl
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string IPListUrl { get; set; }

        public List<ConvertDomainMask> sp_crawls_domain_masks;

        public List<ConvertIPS> sp_crawls_ips;
    }
}