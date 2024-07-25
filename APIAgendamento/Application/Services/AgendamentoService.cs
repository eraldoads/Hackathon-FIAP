using Data.Context;
using Data.Messaging;
using Data.Repository;
using Domain.Entities;
using Domain.Entities.Input;
using Domain.Entities.Output;
using Domain.Interfaces;
using MongoDB.Driver;
using Newtonsoft.Json;
using System.Transactions;

namespace Application.Interfaces
{
    public class AgendamentoService(IAgendamentoRepository agendamentoRepository,
                              IAgendamentoMessageSender agendamentoMessageSender,
                              MongoDBContext context) : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository = agendamentoRepository;
        private readonly IAgendamentoMessageSender _agendamentoMessageSender = agendamentoMessageSender;
        private readonly MongoDBContext _context = context;

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
                Motivo = input.Motivo,
                StatusAgendamento = input.StatusAgendamento,
            };

            using var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

            var novoPedidoCriado = await _agendamentoRepository.PostAgendamentoAsync(agendamento);

            Agendamento agendamentoRabbit = new()
            {
                IdPaciente = input.IdPaciente,
                IdMedico = input.IdMedico,
                DataAgendamento = input.DataAgendamento,
                Motivo = input.Motivo,
                StatusAgendamento = input.StatusAgendamento,
            };

            string message = JsonConvert.SerializeObject(agendamentoRabbit);
            _agendamentoMessageSender.SendMessage("novo_agendamento", message);

            scope.Complete();

            //await _context.Agendamentos.InsertOneAsync(agendamento);
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
            agendamento.StatusAgendamento = input.StatusAgendamento;

            //await _context.Agendamentos.ReplaceOneAsync(agendamento => agendamento.Id == id, agendamento);
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
            agendamento.StatusAgendamento = input?.StatusAgendamento;

            await _context.Agendamentos.ReplaceOneAsync(agendamento => agendamento.Id == id, agendamento);
            return new AgendamentoOutput(agendamento);
        }

        public async Task DeleteAgendamentoAsync(string id)
        {
            await _context.Agendamentos.DeleteOneAsync(agendamento => agendamento.Id == id);
        }

    }
}
