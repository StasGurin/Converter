using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEO.Models
{
    public class ResponsModel
    {
        #region Properties

        public string Title { get; set; }
        public string Body { get; set; }
        public string Keywords { get; set; }
        public bool IsCrawl { get; set; }
        public bool IsForbidden { get; set; }

        #endregion

        #region Constructors

        public ResponsModel()
        {
        }

        #endregion
    }
}