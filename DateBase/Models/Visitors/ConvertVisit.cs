using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Visitors
{
    public class ConvertVisit//Rename to Visit
    {
        public BsonDateTime VisitDateTime { get; set; }
        public List<ConvertVisitPage> VisitPages;
        public List<ConvertVisitPage> RefererPages;//There should be only one referer
    }
}