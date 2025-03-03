using System.Collections.Specialized;

namespace frontendForMasterdev.Models
{
    public class GetAppModel
    {
        public int id { set; get; }
        public string app_Name { set; get; }
        public string app_UrlAddress { set; get; }
        public string iiS_AppName { set; get; }
        public string iiS_AppPoolName { set; get; }
        public string pgsqL_ConnectionString { set; get; }



    }
}