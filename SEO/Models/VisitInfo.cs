using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEO.Models
{
    public class VisitInfo
    {
        #region Properties

        public int ProjectId { get; set; }
        public string UserName { get; set; }
        public string IPAddress { get; set; }
        public DateTime VisitDate { get; set; }
        public string Platform { get; set; }
        public string BrowserType { get; set; }
        public bool isAuthenticated { get; set; }
        public string Url { get; set; }
        public string Referer { get; set; }

        #endregion

        #region Constructors
        public VisitInfo()
        {
        }
        #endregion

    }
}