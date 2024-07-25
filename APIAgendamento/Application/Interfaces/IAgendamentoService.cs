using Domain.Entities.Input;
using Domain.Entities.Output;

namespace Application.Interfaces
{
    public interface IAgendamentoService
    {
        Task<List<AgendamentoOutput>> GetAgendamentoAsync();
        Task<AgendamentoOutput> GetAgendamentoByIdAsync(string id);
        Task<AgendamentoOutput> PostAgendamentoAsync(AgendamentoInput input);
        Task<AgendamentoOutput> PutAgendamentoAsync(string id, AgendamentoInput input);
        Task<AgendamentoOutput> PatchAgendamentoAsync(string id, AgendamentoInput input);
        Task DeleteAgendamentoAsync(string id);

    }
}
