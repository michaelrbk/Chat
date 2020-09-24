using RabbitMQ.Client;
using System.Text;

namespace ChatWebApp.Services
{
    public class SendQueue
    {
        public static void SendMsg(string message)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "botQueue",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "botQueue",
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
