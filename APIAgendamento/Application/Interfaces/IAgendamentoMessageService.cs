namespace Application.Interfaces
{
    public interface IAgendamentoMessageService : IDisposable
    {
        Task ReceberMensagens();
    }
}
