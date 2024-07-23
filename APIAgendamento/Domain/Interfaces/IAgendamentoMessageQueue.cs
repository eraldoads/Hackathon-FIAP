namespace Domain.Interfaces
{
    public interface IAgendamentoMessageQueue
    {
        event Func<string, Task> MessageReceived;
        Task StartListening();
        void ReenqueueMessage(string message);
    }
}
