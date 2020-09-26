using Chat.App.Interfaces;
using Chat.Models;
using Chat.Services.Interfaces;
using System.Threading.Tasks;

namespace Chat.App.App
{
    public class ChatHubApp : IChatHubApp
    {
        private readonly IChatHubService _chatHubService;
        public ChatHubApp(IChatHubService chatHubService)
        {
            _chatHubService = chatHubService;
        }

        public Task SendMessage(Message message)
        {
            _chatHubService.SendMessage(message);
            return Task.CompletedTask;
        }
    }
}
