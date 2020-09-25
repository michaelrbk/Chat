using Chat.App.Interfaces;
using Chat.Services.Interfaces;
using System.Threading.Tasks;

namespace Chat.App.App
{
    public class ChatBotApp : IChatBotApp
    {
        private readonly IChatBotService _chatBotService;
        public ChatBotApp(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }
        public Task ProcessQueue()
        {
            return _chatBotService.ReceiveFromQueue();
        }
    }
}
