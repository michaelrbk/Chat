using Chat.Models;
using System.Threading.Tasks;

namespace Chat.App.Interfaces
{
    public interface IChatHubApp
    {
        public Task SendMessage(Message message);
    }
}
