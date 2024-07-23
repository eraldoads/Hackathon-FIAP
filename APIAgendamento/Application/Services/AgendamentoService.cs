using Data.Context;
using Data.Repository;
using Domain.Entities;
using Domain.Entities.Input;
using Domain.Entities.Output;
using Domain.Interfaces;
using MongoDB.Driver;

namespace Application.Interfaces
{
    public class AgendamentoService : IAgendamentoService
    {
        private readonly MongoDBContext _context;

        public AgendamentoService(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<List<AgendamentoOutput>> GetAgendamentoAsync()
        {
            var agendamentos = await _context.Agendamentos.Find(agendamento => true).ToListAsync();
            return agendamentos.ConvertAll(a => new AgendamentoOutput(a));
        }

        public async Task<AgendamentoOutput> GetAgendamentoByIdAsync(string id)
        {
            var agendamento = await _context.Agendamentos.Find<Agendamento>(agendamento => agendamento.Id == id).FirstOrDefaultAsync();
            return agendamento == null ? null : new AgendamentoOutput(agendamento);
        }

        public async Task<AgendamentoOutput> PostAgendamentoAsync(AgendamentoInput input)
        {
            var agendamento = new Agendamento
            {
                IdPaciente = input.IdPaciente,
                IdMedico = input.IdMedico,
                DataAgendamento = input.DataAgendamento,
                Motivo = input.Motivo
            };

            await _context.Agendamentos.InsertOneAsync(agendamento);
            return new AgendamentoOutput(agendamento);
        }

        public async Task<AgendamentoOutput> PutAgendamentoAsync(string id, AgendamentoInput input)
        {
            var agendamento = await _context.Agendamentos.Find(agendamento => agendamento.Id == id).FirstOrDefaultAsync();

            if (agendamento == null)
            {
                return null;
            }

            agendamento.IdPaciente = input.IdPaciente;
            agendamento.IdMedico = input.IdMedico;
            agendamento.DataAgendamento = input.DataAgendamento;
            agendamento.Motivo = input.Motivo;

            await _context.Agendamentos.ReplaceOneAsync(agendamento => agendamento.Id == id, agendamento);
            return new AgendamentoOutput(agendamento);
        }

        public async Task<AgendamentoOutput> PatchAgendamentoAsync(string id, AgendamentoInput input)
        {
            var agendamento = await _context.Agendamentos.Find(agendamento => agendamento.Id == id).FirstOrDefaultAsync();

            if (agendamento == null)
            {
                return null;
            }

            agendamento.IdPaciente = input.IdPaciente;
            agendamento.IdMedico = input.IdMedico;
            agendamento.DataAgendamento = input.DataAgendamento;
            agendamento.Motivo = input.Motivo;

            await _context.Agendamentos.ReplaceOneAsync(agendamento => agendamento.Id == id, agendamento);
            return new AgendamentoOutput(agendamento);
        }

        public async Task DeleteAgendamentoAsync(string id)
        {
            await _context.Agendamentos.DeleteOneAsync(agendamento => agendamento.Id == id);
        }


        //private readonly IAgendamentoRepository _agendamentoRepository;
        ////private readonly IAgendamentoMessageSender _agendamentoMessageSender;

        //public AgendamentoService(IAgendamentoRepository agendamentoRepository
        //                          //IAgendamentoMessageSender agendamentoMessageSender
        //                          )
        //{
        //    _agendamentoRepository = agendamentoRepository;
        //    //_agendamentoMessageSender = agendamentoMessageSender;
        //}

        ///// <summary>
        ///// Obtém todos os pedidos.
        ///// </summary>
        ///// <returns>Retorna uma lista de pedidos.</returns>
        //public async Task<List<AgendamentoOutput>> GetAgendamentoAsync()
        //{
        //    var agendamentos = await _agendamentoRepository.GetAgendamentos();

        //    return agendamentos.Select(a => new AgendamentoOutput
        //    {
        //        IdAgendamento = a.IdAgendamento,
        //        IdPaciente = a.IdPaciente,
        //        IdMedico = a.IdMedico,
        //        DataAgendamento = a.DataAgendamento,
        //        Motivo = a.Motivo,
        //    }).ToList();
        //}


        //public async Task<AgendamentoOutput?> GetAgendamentoByIdAsync(int id)
        //{
        //    var agendamento = await _agendamentoRepository.GetAgendamentoByIdAsync(id);
        //    if (agendamento == null)
        //    {
        //        return null;
        //    }
        //    return new AgendamentoOutput
        //    {
        //        IdAgendamento = agendamento.IdAgendamento,
        //        IdPaciente = agendamento.IdPaciente,
        //        IdMedico = agendamento.IdMedico,
        //        DataAgendamento = agendamento.DataAgendamento,
        //        Motivo = agendamento.Motivo,
        //    };
        //}

        //public async Task<AgendamentoOutput> PostAgendamentoAsync(AgendamentoInput input)
        //{
        //    var agendamento = new Agendamento
        //    {
        //        IdPaciente = input.IdPaciente,
        //        IdMedico = input.IdMedico,
        //        DataAgendamento = input.DataAgendamento,
        //        Motivo = input.Motivo
        //    };

        //    await _agendamentoRepository.PostAgendamentoAsync(agendamento);

        //    return new AgendamentoOutput
        //    {
        //        IdAgendamento = agendamento.IdAgendamento,
        //        IdPaciente = agendamento.IdPaciente,
        //        IdMedico = agendamento.IdMedico,
        //        DataAgendamento = agendamento.DataAgendamento,
        //        Motivo = agendamento.Motivo,
        //    };
        //}

        //public async Task<AgendamentoOutput?> PutAgendamentoAsync(int id, AgendamentoInput input)
        //{
        //    var agendamento = await _agendamentoRepository.GetAgendamentoByIdAsync(id);
        //    if (agendamento == null)
        //    {
        //        return null;
        //    }

        //    agendamento.IdPaciente = input.IdPaciente;
        //    agendamento.IdMedico = input.IdMedico;
        //    agendamento.DataAgendamento = input.DataAgendamento;
        //    agendamento.Motivo = input.Motivo;

        //    await _agendamentoRepository.PutAgendamentoAsync(agendamento);

        //    return new AgendamentoOutput
        //    {
        //        IdAgendamento = agendamento.IdAgendamento,
        //        IdPaciente = agendamento.IdPaciente,
        //        IdMedico = agendamento.IdMedico,
        //        DataAgendamento = agendamento.DataAgendamento,
        //        Motivo = agendamento.Motivo,
        //    };
        //}


        //public async Task<AgendamentoOutput?> PatchAgendamentoAsync(int id, AgendamentoInput input)
        //{
        //    var agendamento = await _agendamentoRepository.GetAgendamentoByIdAsync(id);
        //    if (agendamento == null)
        //    {
        //        return null;
        //    }

        //    agendamento.IdPaciente = input.IdPaciente;
        //    agendamento.IdMedico = input.IdMedico;
        //    agendamento.DataAgendamento = input.DataAgendamento;
        //    agendamento.Motivo = input.Motivo;

        //    await _agendamentoRepository.PatchAgendamentoAsync(agendamento);

        //    return new AgendamentoOutput
        //    {
        //        IdAgendamento = agendamento.IdAgendamento,
        //        IdPaciente = agendamento.IdPaciente,
        //        IdMedico = agendamento.IdMedico,
        //        DataAgendamento = agendamento.DataAgendamento,
        //        Motivo = agendamento.Motivo,
        //    };
        //}

        //public async Task DeleteAgendamentoAsync(int id)
        //{
        //    var agendamento = await _agendamentoRepository.GetAgendamentoByIdAsync(id);
        //    if (agendamento != null)
        //    {
        //        await _agendamentoRepository.DeleteAgendamentoAsync(agendamento);
        //    }
        //}

    }
}
