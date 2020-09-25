using Chat.Models;
using Chat.Models.Stock;
using Chat.WebService.Interfaces;
using Microsoft.Extensions.Options;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Chat.WebService.Services
{
    public class StockWebService : IStockWebService
    {
        private static Settings _settings;
        public StockWebService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }
        public Task<Stock> GetStock(StockRequest request)
        {
            var stock = new Stock();
            stock.Symbol = GetCSV(_settings.StockWebServiceUrl.Replace("{stock_code}", request.StockCode));
            return Task.FromResult(stock);
        }

        private string GetCSV(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();

            StreamReader sr = new StreamReader(resp.GetResponseStream());
            string results = sr.ReadToEnd();
            sr.Close();

            return results;
        }
    }
}
