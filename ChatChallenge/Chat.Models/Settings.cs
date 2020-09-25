namespace Chat.Models
{
    public class Settings
    {
        public QueueSettings QueueSettings { get; set; }
        public string StockWebServiceUrl { get; set; }
    }
    public class QueueSettings
    {
        public string Address { get; set; }
    }
}
