using Chat.Models;
using System.Threading.Tasks;

namespace Chat.App.Interfaces
{
    public interface IChatHubApp
    {
        Task MessageReceived(Message message);
    }
}
