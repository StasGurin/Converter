namespace DataBase.Models.Crawls
{
    public class IP
    {
        #region Properties

        public string Type { get; set; }
        public string Address { get; set; }

        #endregion

        #region Constructors

        public IP()
        {
        }

        public IP(string type, string address)
        {
            Type = type;
            Address = address;
        }

        #endregion
    }
}