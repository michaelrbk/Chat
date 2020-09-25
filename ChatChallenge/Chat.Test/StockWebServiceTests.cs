using Chat.Models;
using Chat.Models.Stock;
using Chat.WebService.Services;
using Microsoft.Extensions.Options;
using Xunit;

namespace Chat.Test
{
    public class StockWebServiceTests
    {

        [Fact]
        public void StockWebServiceTest()
        {
            Settings _settings = new Settings() { StockWebServiceUrl = "https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv" };
            IOptions<Settings> options = Options.Create(_settings);
            StockWebService stockWS = new StockWebService(options);

            var stockRequest = new StockRequest();
            stockRequest.StockCode = "aapl.us";

            Assert.True(stockWS.GetStock(stockRequest).Result.Symbol.Equals("aapl.us"));
        }
    }
}
