using Chat.Models;
using Chat.QueueProducer.Services;
using Chat.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Chat.Services.Services
{
    public class ChatHubService : IChatHubService
    {
        private static IOptions<Settings> _settings;

        public ChatHubService(IOptions<Settings> settings)
        {
            _settings = settings;
        }
        private static QueueProducerService QueueProducerService()
        {
            QueueProducerService queueProducer = new QueueProducerService(_settings);
            return queueProducer;
        }

        public async Task<bool> SendMessage(Message message)
        {
            //If starts with / its a command. Should be sended to the ChatBot
            if (message.MessageText.Trim().StartsWith("/"))
            {
                var queueMessage = new QueueMessage
                {
                    Message = message.MessageText,
                    Queue = "ChatBot"
                };
                QueueProducerService().AddToQueue(queueMessage);
                return false;
            }

            return true;
        }
    }
}
