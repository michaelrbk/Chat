using Chat.Models;
using Chat.QueueManager.Services;
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
        private static QueueManagerService QueueManagerService()
        {
            QueueManagerService queueManager = new QueueManagerService(_settings);
            return queueManager;
        }

        public Task MessageReceived(Message message)
        {
            //If starts with / its a command. Should be sended to the ChatBot
            if (message.MessageText.StartsWith("/"))
            {
                var queueMessage = new QueueMessage
                {
                    Message = message.MessageText,
                    Queue = "ChatBot"
                };
                QueueManagerService().AddToQueue(queueMessage);
            }
            return Task.CompletedTask;
        }
    }
}
