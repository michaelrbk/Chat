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
        private static ITestOutputHelper _output;
        private static Settings _settings;
        private static IOptions<Settings> _options;
        private static StockWebService _stockWS;
        public StockWebServiceTests(ITestOutputHelper output)
        {
            _output = output;
            //Set URL of the WS
            _settings = new Settings() { StockWebServiceUrl = "https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv" };
            _options = Options.Create(_settings);
            _stockWS = new StockWebService(_options);
        }

        [Fact]
        public void StockWebServiceTest()
        {
            var stockRequest = new StockRequest
            {
                StockCode = "aapl.US"
            };
            Stock stock = _stockWS.GetStock(stockRequest).Result;
            _output.WriteLine(JsonSerializer.Serialize(stock, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            }));

            Assert.True(stock.Symbol.Equals("AAPL.US"));
        }
    }
}
