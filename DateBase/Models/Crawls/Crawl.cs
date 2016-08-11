using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Crawls
{
    public class Crawl
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string IPListUrl { get; set; }

        public List<string> DomainMasks;

        public List<ConvertIPS> CrawlIP;
    }
}