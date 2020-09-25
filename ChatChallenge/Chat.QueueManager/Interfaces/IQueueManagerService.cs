using Chat.Models;
using System.Threading.Tasks;

namespace Chat.QueueManager.Interfaces
{
    public interface IQueueManagerService
    {
        Task AddToQueue(QueueMessage queueMessage);
        Task GetFromQueue(string queue);
    }
}
