using Chat.Models;
using Chat.QueueConsumer.Services;
using Chat.Services.Interfaces;
using Microsoft.Extensions.Options;

namespace Chat.Services.Services
{
    public class ChatBotService : IChatBotService
    {
        private static IOptions<Settings> _settings;
        public ChatBotService(IOptions<Settings> settings)
        {
            _settings = settings;
        }

        private static QueueConsumerService QueueConsumerService()
        {
            QueueConsumerService queueManager = new QueueConsumerService(_settings);
            return queueManager;
        }

        public void ReceiveFromQueue()
        {
            QueueConsumerService().GetFromQueue("ChatBot");
        }
    }
}
