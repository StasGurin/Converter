using MongoDB.Bson;

namespace SEO.Models
{
    public class PageRenderingInfo
    {
        #region Properties

        public ObjectId? Id { get; set; }
        public bool IsCrawl { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Keywords { get; set; }

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

        public PageRenderingInfo(string title, string body, string keywords)
        {
            Title = title;
            Body = body;
            Keywords = keywords;
        }

        public PageRenderingInfo(ObjectId? id, bool isCrawl, string title, string body, string keywords)
        {
            Id = id;
            IsCrawl = isCrawl;
            Title = title;
            Body = body;
            Keywords = keywords;
        }
        #endregion
    }
}