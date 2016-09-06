using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEO.Models
{
    public class CrawlInfo
    {
        #region Properties

        public ObjectId? Id { get; set; }
        public bool IsCrawl { get; set; }

        #endregion

        #region Constructors

        public CrawlInfo()
        {
        }

        public CrawlInfo(ObjectId? id, bool isCrawl)
        {
            Id = id;
            IsCrawl = isCrawl;
        }
        #endregion
    }
}