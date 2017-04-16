﻿using MongoDB.Bson;

namespace ConverterTest.Models.Crawls
{
    public class SourceCrawl
    {
        #region Properties

        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public string Name { get; set; }
        public string IPListUrl { get; set; }

        #endregion
    }

}