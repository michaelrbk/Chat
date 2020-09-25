namespace Chat.QueueConsumer.Interfaces
{
    public interface IQueueConsumerService
    {
        void GetFromQueue(string queue);
    }
}
