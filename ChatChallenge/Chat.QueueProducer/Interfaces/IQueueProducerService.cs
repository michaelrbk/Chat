using Chat.Models;
using System.Threading.Tasks;

namespace Chat.QueueProducer.Interfaces
{
    public interface IQueueProducerService
    {
        Task AddToQueue(QueueMessage queueMessage);
    }
}
