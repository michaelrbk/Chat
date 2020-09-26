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
            string identityName = Context.User.Identity.Name ?? "anonymous";
            await _chatHubApp.SendMessage(new Message { MessageText = message, IdentityName = identityName });

            if (!message.Trim().StartsWith("/"))
            {
                await Clients.All.SendAsync("ReceiveMessage", identityName, message);
            }
        }
    }
}
