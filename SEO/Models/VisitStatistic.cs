using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEO.Models
{
    public class VisitStatistic
    {
        #region Properties

        public int UsersNumber { get; set; }
        public int CrawlsNumber { get; set; }
        public int AdminsNumber { get; set; }
        public int BotsNumber { get; set; }
        public int ForbiddenNumber { get; set; }

        #endregion

        #region Constructors

        public VisitStatistic()
        {

        } 

        public VisitStatistic(int usersNumber, int crawlsNumber, int adminsNumber, int botsNumber, int forbiddenNumber)
        {
            UsersNumber = usersNumber;
            CrawlsNumber = crawlsNumber;
            AdminsNumber = adminsNumber;
            BotsNumber = botsNumber;
            ForbiddenNumber = forbiddenNumber;
        }

        #endregion

    }
}