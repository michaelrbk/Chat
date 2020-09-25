using Chat.Models;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IChatHubService
    {
        public Task MessageReceived(Message message);
    }
}
