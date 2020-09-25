namespace Chat.QueueConsumer.Interfaces
{
    public interface IQueueConsumerService
    {
        string GetOneFromQueue(string queue);
        void GetFromQueue(string queue);
    }
}
