using System;

namespace SEO.Models
{
    public class DateStatModel
    {
        #region Properties

        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string DomainName { get; set; }

        #endregion

        #region Constructors

        public DateStatModel()
        {

        }

        public DateStatModel(DateTime startDate, DateTime finishDate, string domainName)
        {
            StartDate = startDate;
            FinishDate = finishDate;
            DomainName = domainName;
        }

        #endregion
    }
}