using MongoDB.Bson;
using System.Collections.Generic;

namespace Visitors.Models
{
    public class Crawl
    {
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public string Name { get; set; }
        public string IPListUrl { get; set; }

        public List<DomainMask> sp_crawls_domain_masks;

        public List<IPS> sp_crawls_ips;
    }

}