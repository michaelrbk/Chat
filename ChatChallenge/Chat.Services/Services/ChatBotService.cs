using Chat.Models;
using Chat.QueueManager.Services;
using Chat.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace Chat.Services.Services
{
    public class ChatBotService : IChatBotService
    {
        private static IOptions<Settings> _settings;
        public ChatBotService(IOptions<Settings> settings)
        {
            _settings = settings;
        }

        private static QueueManagerService QueueManagerService()
        {
            QueueManagerService queueManager = new QueueManagerService(_settings);
            return queueManager;
        }

        public Task ReceiveFromQueue()
        {
            QueueManagerService().GetFromQueue("ChatBot");
            return Task.CompletedTask;
        }
    }
}
