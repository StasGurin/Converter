using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Visitors
{
    public class Visit
    {
        public BsonDateTime VisitDateTime { get; set; }
        public List<VisitPage> VisitPages;
        public string RefererPages;
    }
}