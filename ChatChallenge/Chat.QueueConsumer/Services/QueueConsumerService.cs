using Chat.CommandInterpreter.Services;
using Chat.Models;
using Chat.QueueConsumer.Interfaces;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace Chat.QueueConsumer.Services
{
    public class QueueConsumerService : IQueueConsumerService
    {
        private static Settings _settings;
        private static IModel _channel;
        private static CommandInterpreterService _command;
        public QueueConsumerService(IOptions<Settings> settings)
        {
            _settings = settings.Value;
            var factory = new ConnectionFactory() { HostName = _settings.QueueUrl };
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _command = new CommandInterpreterService(settings);
        }
        public string GetOneFromQueue(string queue)
        {
            _channel.QueueDeclare(queue: queue,
                                durable: false,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var data = _channel.BasicGet(queue, true);
            var message = Encoding.UTF8.GetString(data.Body.ToArray());
            return message;
        }

        public void GetFromQueue(string queue)
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
                    _command.InterpretCommand(message);
                }
                Console.WriteLine(" [x] Received {0}", message);
            };
            _channel.BasicConsume(queue: queue,
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
