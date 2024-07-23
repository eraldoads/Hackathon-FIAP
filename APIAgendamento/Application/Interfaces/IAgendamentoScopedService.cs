namespace Application.Interfaces
{
    public interface IAgendamentoScopedService
    {
        Task DoWork(CancellationToken cancellationToken);
    }
}
