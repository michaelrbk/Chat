using Chat.Models;
using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IChatHubService
    {
        public Task<bool> SendMessage(Message message);
    }
}
