using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Data.Repository
{
    public class AgendamentoRepository(MongoDBContext context) : IAgendamentoRepository
    {
        private readonly MongoDBContext _context = context;

        public async Task<List<Agendamento>> GetAgendamentos()
        {
            return await _context.Agendamentos.Find(agendamento => true).ToListAsync();
        }

        public async Task<Agendamento?> GetAgendamentoByIdAsync(string id)
        {
            return await _context.Agendamentos.Find<Agendamento>(agendamento => agendamento.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Agendamento> PostAgendamentoAsync(Agendamento agendamento)
        {
            await _context.Agendamentos.InsertOneAsync(agendamento);

            return agendamento;
        }

        public async Task PutAgendamentoAsync(Agendamento agendamento)
        {
            await _context.Agendamentos.ReplaceOneAsync(a => a.Id == agendamento.Id, agendamento);
        }

        public async Task PatchAgendamentoAsync(Agendamento agendamento)
        {
            await _context.Agendamentos.ReplaceOneAsync(a => a.Id == agendamento.Id, agendamento);
        }

        public async Task DeleteAgendamentoAsync(Agendamento agendamento)
        {
            await _context.Agendamentos.DeleteOneAsync(a => a.Id == agendamento.Id);
        }
    }
}
