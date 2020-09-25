using Chat.Models;
using Chat.QueueProducer.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Threading.Tasks;

namespace Chat.QueueProducer.Services
{
    public class QueueProducerService : IQueueProducerService
    {
        private static Settings _settings;
        private static IModel _channel;
        public QueueProducerService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            var factory = new ConnectionFactory() { HostName = _settings.QueueUrl };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
        }
        public Task AddToQueue(QueueMessage queueMessage)
        {
            _channel.QueueDeclare(queue: queueMessage.Queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(queueMessage.Message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: queueMessage.Queue,
                                 basicProperties: null,
                                 body: body);
            return Task.CompletedTask;
        }
    }
}
