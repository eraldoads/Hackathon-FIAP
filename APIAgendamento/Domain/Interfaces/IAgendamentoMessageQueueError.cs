namespace Domain.Interfaces
{
    public interface IAgendamentoMessageQueueError
    {
        event Func<string, Task> MessageReceived;
        Task StartListening();
        void ReenqueueMessage(string message);
    }
}
