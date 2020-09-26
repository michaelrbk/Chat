using Chat.Models;
using ChatWebApp.Hub;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChatWebApp.Background
{
    public class ConsumeQueueHostedService : BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IHubContext<ChatHub> _hub;
        private static Settings _settings;

        public ConsumeQueueHostedService(ILoggerFactory loggerFactory, IOptions<Settings> settings, IHubContext<ChatHub> hub)
        {
            _settings = settings.Value;
            _logger = loggerFactory.CreateLogger<ConsumeQueueHostedService>();
            var factory = new ConnectionFactory { HostName = _settings.QueueUrl };
            _connection = factory.CreateConnection();
            var connection = factory.CreateConnection();
            _channel = connection.CreateModel();
            _hub = hub;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            _channel.QueueDeclare(queue: "ChatHub",
                               durable: false,
                               exclusive: false,
                               autoDelete: false,
                               arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
           {
               var body = ea.Body.ToArray();
               var message = Encoding.UTF8.GetString(body);
               _hub.Clients.All.SendAsync("ReceiveMessage", "ChatBot", message);
               _logger.LogInformation("Consume ChatHub msg: {0}", message);
           };
            _channel.BasicConsume(queue: "ChatHub",
                                 autoAck: true,
                                 consumer: consumer);
        }
    }
}
