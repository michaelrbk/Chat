using Chat.App.Interfaces;
using Chat.Services.Interfaces;

namespace Chat.App.App
{
    public class ChatBotApp : IChatBotApp
    {
        private readonly IChatBotService _chatBotService;
        public ChatBotApp(IChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }
        public void ProcessQueue()
        {
            _chatBotService.ReceiveFromQueue();
        }
    }
}
