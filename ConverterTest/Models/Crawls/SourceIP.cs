﻿using MongoDB.Bson;

namespace ConverterTest.Models.Crawls
{
    public class SourceIP
    {
        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public string IPType { get; set; }
        public string IPAddress { get; set; }
    }
}