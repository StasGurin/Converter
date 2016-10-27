namespace SEO.Models
{
    public class ResponseModel
    {
        #region Properties

        public string Title { get; set; }
        public string Body { get; set; }
        public string Keywords { get; set; }
        public bool IsCrawl { get; set; }
        public bool IsForbidden { get; set; }

        #endregion

        #region Constructors

        public ResponseModel()
        {
        }

        public ResponseModel(string title, string body, string keywords, bool isCrawl, bool isForbidden)
        {
            Title = title;
            Body = body;
            Keywords = keywords;
            IsCrawl = isCrawl;
            IsForbidden = isForbidden;
        }
        #endregion
    }
}