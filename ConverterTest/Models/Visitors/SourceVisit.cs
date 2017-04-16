using MongoDB.Bson;
using System.Collections.Generic;

namespace ConverterTest.Models.Visitors
{
    public class SourceVisit
    {
        #region Properties

        public ObjectId Id { get; set; }
        public int UniqueID { get; set; }
        public int OwnerID { get; set; }
        public BsonDateTime VisitDateTime { get; set; }
        public int PageID { get; set; }
        public int RefererPageID { get; set; }
        public List<SourceVisitPage> sp_visit_pages;

        #endregion
    }
}