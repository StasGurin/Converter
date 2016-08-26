namespace DataBase.Models.Visitors
{
    public class VisitPage
    {
        #region Properties

        public string Url { get; set; }

        #endregion

        #region Constructors

        public VisitPage()
        {
        }

        public VisitPage(string url)
        {
            Url = url;
        }

        #endregion
    }
}