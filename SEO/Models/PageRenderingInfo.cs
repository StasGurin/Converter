using MongoDB.Bson;

namespace SEO.Models
{
    public class PageRenderingInfo
    {
        #region Properties

        public ObjectId? Id { get; set; }
        public bool IsCrawl { get; set; }

        #endregion

        #region Constructors

        public PageRenderingInfo()
        {
        }

        public PageRenderingInfo(ObjectId? id, bool isCrawl)
        {
            Id = id;
            IsCrawl = isCrawl;
        }
        #endregion
    }
}