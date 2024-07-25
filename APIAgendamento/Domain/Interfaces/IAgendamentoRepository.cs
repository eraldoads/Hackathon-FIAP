using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<List<Agendamento>> GetAgendamentos();
        Task<Agendamento?> GetAgendamentoByIdAsync(string id);
        Task<Agendamento> PostAgendamentoAsync(Agendamento agendamento);
        Task PutAgendamentoAsync(Agendamento agendamento);
        Task PatchAgendamentoAsync(Agendamento agendamento);
        Task DeleteAgendamentoAsync(Agendamento agendamento);
    }
}
