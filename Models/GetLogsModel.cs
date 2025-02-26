namespace frontendForMasterdev.Models
{
    public class GetLogsModel
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int appid { get; set; }
        public string message { get; set; }
    }
}