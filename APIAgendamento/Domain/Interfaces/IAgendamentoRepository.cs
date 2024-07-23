using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAgendamentoRepository
    {
        Task<List<Agendamento>> GetAgendamentos();
        Task<Agendamento?> GetAgendamentoByIdAsync(string id);
        Task PostAgendamentoAsync(Agendamento agendamento);
        Task PutAgendamentoAsync(Agendamento agendamento);
        Task PatchAgendamentoAsync(Agendamento agendamento);
        Task DeleteAgendamentoAsync(Agendamento agendamento);
    }
}
