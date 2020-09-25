using Chat.Models;

namespace Chat.QueueProducer.Interfaces
{
    public interface IQueueProducerService
    {
        void AddToQueue(QueueMessage queueMessage);
    }
}
