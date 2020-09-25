using System.Threading.Tasks;

namespace Chat.Services.Interfaces
{
    public interface IChatBotService
    {
        public Task ReceiveFromQueue();
    }
}
