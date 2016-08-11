using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBase.Models.Visitors
{
    public class ConvertVisit
    {
        public ObjectId Id { get; set; }
        public BsonDateTime VisitDateTime { get; set; }
        public List<ConvertVisitPage> sp_visit_pages;
        public List<ConvertVisitPage> sp_visit_referer_pages;
    }
}