namespace DataBase.Models.Projects
{
    public class PageInfo
    {
        #region Properties

        public string Url { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Keywords { get; set; }

        #endregion

        #region Constructors

        public PageInfo()
        {
        }

        public PageInfo(string url, string type, string title, string body, string keywords)
        {
            Url = url;
            Type = type;
            Title = title;
            Body = body;
            Keywords = keywords;
        }

        #endregion
    }
}