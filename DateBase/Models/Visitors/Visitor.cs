using System;
using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Visitors
{
    public class Visitor
    {
        #region Properties

        public ObjectId Id { get; set; }
        public DateTime VisitDate { get; set; }
        public string IPAddress { get; set; }
        public ObjectId? CrawlID { get; set; }
        public string DNS { get; set; }
        public UserInfo UserInfo { get; set; }
        public bool IsBot { get; set; }
        public bool IsForbidden { get; set; }
        public List<Visit> Visits;

        #endregion

        #region Constructors

        public Visitor()
        {
        }

        public Visitor(DateTime visitDate, string ipAddress, string dns)
        {
            VisitDate = visitDate;
            IPAddress = ipAddress;
            DNS = dns;
            Visits = new List<Visit>();
            UserInfo = new UserInfo();
        }

        public Visitor(ObjectId id, DateTime visitDate, string ipAddress, string dns)
        {
            Id = id;
            VisitDate = visitDate;
            IPAddress = ipAddress;
            DNS = dns;
            Visits = new List<Visit>();
            UserInfo = new UserInfo();
        }

        #endregion
    }
}