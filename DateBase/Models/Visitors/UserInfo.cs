using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataBase.Models.Visitors
{
    public class UserInfo
    {
        public string Platform { get; set; }
        public string BrowserType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public string type { get; set; }


    }
}