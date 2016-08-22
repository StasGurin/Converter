using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Visitors
{
    public class ConvertVisitor//Rename to visitor
    {
        public ObjectId Id { get; set; }
        public BsonDateTime VisitDate { get; set; }
        public string IPAddress { get; set; }
        public ObjectId? CrawlID { get; set; }
        public string DNS { get; set; }
        public UserInfo UserInfo { get; set; }
        public bool IsBot { get; set; }
        public bool IsForbidden { get; set; }
        public List<ConvertVisit> Visits;
    }
}