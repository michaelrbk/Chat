using Chat.Models;
using Chat.Services.Interfaces;
using Chat.Services.Services;
using Microsoft.Extensions.Options;
using Xunit;
using Xunit.Abstractions;

namespace Chat.Test
{
    public class ChatHubTests
    {
        private static ITestOutputHelper _output;
        private static Settings _settings;
        private static IOptions<Settings> _options;
        private readonly IChatHubService _chatHubService;
        public ChatHubTests(ITestOutputHelper output)
        {
            _output = output;
            //Set URL of the WS

            _settings = new Settings() { QueueUrl = "localhost", StockWebServiceUrl = "https://stooq.com/q/l/?s={stock_code}&f=sd2t2ohlcv&h&e=csv" };
            _options = Options.Create(_settings);
            _chatHubService = new ChatHubService(_options);
        }
        [Fact]
        public void ChatBotTest()
        {
            // _chatHubService.ReceiveFromQueue();
            _output.WriteLine("teste");
            Assert.True(true);
        }
    }
}
