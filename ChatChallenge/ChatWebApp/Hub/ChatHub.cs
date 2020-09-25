using Chat.App.Interfaces;
using Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatWebApp.Hub
{
    [Authorize]
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub
    {
        private readonly IChatHubApp _chatHubApp;
        public ChatHub(IChatHubApp chatHubApp)
        {
            _chatHubApp = chatHubApp;
        }
        public async Task SendMessage(string message)
        {
            ///stock=stock_code
            if (message.StartsWith("/"))
            {
                await _chatHubApp.MessageReceived(new Message { MessageText = message });
            }
            else
            {
                await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message);
            }
        }
    }
}
