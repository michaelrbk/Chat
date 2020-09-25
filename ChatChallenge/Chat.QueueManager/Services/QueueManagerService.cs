using Chat.Models;
using Chat.QueueManager.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Chat.QueueManager.Services
{
    public class QueueManagerService : IQueueManagerService
    {
        private static Settings _settings;
        private static IModel _channel;
        private static CommandInterpreterServices _command;
        public QueueManagerService(IOptions<Settings> settings)
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

        public Task GetFromQueue(string queue)
        {

            _channel.QueueDeclare(queue: queue,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                if (queue == "ChatBot")
                {
                    CommandInterpreterServices cmd = new CommandInterpreterServices();
                }
                Console.WriteLine(" [x] Received {0}", message);
            };
            _channel.BasicConsume(queue: queue,
                                 autoAck: true,
                                 consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
