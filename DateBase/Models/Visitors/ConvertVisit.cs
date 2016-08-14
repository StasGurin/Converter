using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBase.Models.Visitors
{
    public class ConvertVisit
    {
        public BsonDateTime VisitDateTime { get; set; }
        public List<ConvertVisitPage> VisitPages;
        public List<ConvertVisitPage> RefererPages;
    }
}