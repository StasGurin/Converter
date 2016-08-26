using MongoDB.Bson;
using System.Collections.Generic;

namespace DataBase.Models.Visitors
{
    public class Visit
    {
        #region Properties

        public BsonDateTime VisitDateTime { get; set; }
        public List<VisitPage> VisitPages;
        public string RefererPage;

        #endregion

        #region Constructors

        public Visit()
        {
        }

        public Visit(BsonDateTime visitDateTime)
        {
            VisitDateTime = visitDateTime;
            VisitPages = new List<VisitPage>();
        }

        #endregion
    }
}