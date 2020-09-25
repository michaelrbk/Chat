using Chat.Models;
using System.Threading.Tasks;

namespace Chat.QueueManager.Interfaces
{
    public interface IQueueManagerService
    {
        public Task AddToQueue(QueueMessage queueMessage);
        public Task GetFromQueue(string queue);
    }
}
