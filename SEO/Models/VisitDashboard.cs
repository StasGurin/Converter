using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEO.Models
{
    public class VisitDashboard
    {
        #region Properties

        public string Time { get; set; }
        public string Url { get; set; }
        public string RefererPage { get; set; }

        #endregion

        #region Constructors

        public VisitDashboard()
        {

        }

        public VisitDashboard(DateTime time, string url, string refererPage)
        {
            Time = time.ToString(@"hh\:mm\:ss");
            Url = url;
            RefererPage = refererPage;
        }

        #endregion
    }
}