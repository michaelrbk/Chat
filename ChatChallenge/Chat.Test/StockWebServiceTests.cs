using Chat.Models;
using Chat.Models.Stock;
using Chat.WebService.Services;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Chat.Test
{
    public class StockWebServiceTests
    {
        private readonly ITestOutputHelper _output;

        public StockWebServiceTests(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void StockWebServiceTest()
        {
            //Set URL of the WS
            Settings _settings = new Settings() { StockWebServiceUrl = "https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv" };
            IOptions<Settings> options = Options.Create(_settings);
            StockWebService stockWS = new StockWebService(options);

            var stockRequest = new StockRequest();
            stockRequest.StockCode = "aapl.US";
            Stock stock = stockWS.GetStock(stockRequest).Result;
            _output.WriteLine(JsonSerializer.Serialize(stock, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            }));

            Assert.True(stock.Symbol.Equals("AAPL.US"));
        }
    }
}
