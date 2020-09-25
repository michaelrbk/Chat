using System.Threading.Tasks;

namespace Chat.App.Interfaces
{
    public interface IChatBotApp
    {
        public Task ProcessQueue();
    }
}
