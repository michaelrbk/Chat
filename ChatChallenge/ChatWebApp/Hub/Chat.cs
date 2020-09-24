using ChatWebApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace ChatWebApp.Hub
{
    [Authorize]
    public class Chat : Microsoft.AspNetCore.SignalR.Hub
    {
        public async Task SendMessage(string message)
        {
            ///stock=stock_code
            if (message.StartsWith("/"))
            {
                SendQueue.SendMsg(message);
            }
            else
            {
                await Clients.All.SendAsync("ReceiveMessage", Context.User.Identity.Name ?? "anonymous", message);
            }
        }
    }
}
