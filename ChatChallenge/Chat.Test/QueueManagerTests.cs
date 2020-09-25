using Chat.Models;
using Chat.QueueConsumer.Services;
using Chat.QueueProducer.Services;
using Microsoft.Extensions.Options;
using System.Text.Json;
using Xunit;
using Xunit.Abstractions;

namespace Chat.Test
{
    public class QueueManagerTests
    {
        private static ITestOutputHelper _output;
        private static Settings _settings;
        private static IOptions<Settings> _options;
        private static QueueConsumerService _queueConsumerService;
        private static QueueProducerService _queueProducerService;
        public QueueManagerTests(ITestOutputHelper output)
        {
            _output = output;
            //Set URL of the WS

            _settings = new Settings() { QueueUrl = "localhost" };
            _options = Options.Create(_settings);
            _queueConsumerService = new QueueConsumerService(_options);
            _queueProducerService = new QueueProducerService(_options);
        }

        [Fact]
        public void AddQueueTest()
        {
            var queueMessage = new QueueMessage
            {
                Message = "/stock=aapl.us",
                Queue = "ChatBot"
            };
            _queueProducerService.AddToQueue(queueMessage);
            _output.WriteLine(JsonSerializer.Serialize(queueMessage));
            Assert.True(true);
        }
        [Fact]
        public void GetQueueTest()
        {
            string Queue = "ChatBot";
            _queueConsumerService.GetFromQueue(Queue);
            _output.WriteLine(JsonSerializer.Serialize(Queue));

            Assert.True(true);
        }

        [Fact]
        public void GetOneFromQueueTest()
        {
            string Queue = "ChatHub";
            _output.WriteLine(JsonSerializer.Serialize(_queueConsumerService.GetOneFromQueue(Queue)));

            Assert.True(true);
        }
    }
}
