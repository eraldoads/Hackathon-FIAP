namespace Domain.Interfaces
{
    public interface IAgendamentoMessageSender
    {
        void SendMessage(string queueName, string message);
    }
}
