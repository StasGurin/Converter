using System;

namespace SEO.Models
{
    public class VisitorDashboard
    {
        #region Properties

        public string Country { get; set; }
        public string City { get; set; }
        public string UserName { get; set; }
        public string IP { get; set; }
        public string DNS { get; set; }
        public int Pages { get; set; }
        public string Time { get; set; }
        public string Duration { get; set; }
        public string RefererPage { get; set; }
        public string BrowserType { get; set; }
        public string Platform { get; set; }

        #endregion
        
        #region Constructors

        public VisitorDashboard()
        {

        }
        public VisitorDashboard(string country, string city, string userName, string ip, string dns, int pages, DateTime time,
            double duration, string refererPage, string browserType, string platform)
        {
            Country = country;
            City = city;
            UserName = userName;
            IP = ip;
            DNS = dns;
            Pages = pages;
            Time = time.ToShortTimeString();
            Duration = String.Format("{0}:{1:0}", (int)duration, (duration - (int)duration) * 60);
            RefererPage = refererPage;
            BrowserType = browserType;
            Platform = platform;
        }

        #endregion
    }
}