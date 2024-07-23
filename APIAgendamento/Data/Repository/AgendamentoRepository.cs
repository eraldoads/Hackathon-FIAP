using Data.Context;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Data.Repository
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly MongoDBContext _context;

        public AgendamentoRepository(MongoDBContext context)
        {
            _context = context;
        }

        public async Task<List<Agendamento>> GetAgendamentos()
        {
            return await _context.Agendamentos.Find(agendamento => true).ToListAsync();
        }

        public async Task<Agendamento?> GetAgendamentoByIdAsync(string id)
        {
            return await _context.Agendamentos.Find<Agendamento>(agendamento => agendamento.Id == id).FirstOrDefaultAsync();
        }

        public async Task PostAgendamentoAsync(Agendamento agendamento)
        {
            await _context.Agendamentos.InsertOneAsync(agendamento);
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

        //private readonly MySQLContextAgendamento _context;

        //public AgendamentoRepository(MySQLContextAgendamento context)
        //{
        //    _context = context;
        //}

        //public async Task<List<Agendamento>> GetAgendamentos()
        //{
        //    return await _context.Agendamentos
        //        .ToListAsync();
        //}

        //public async Task<Agendamento?> GetAgendamentoByIdAsync(int id)
        //{
        //    return await _context.Agendamentos
        //        .FirstOrDefaultAsync(a => a.IdAgendamento == id);
        //}


        //public async Task PostAgendamentoAsync(Agendamento agendamento)
        //{
        //    await _context.Agendamentos.AddAsync(agendamento);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task PutAgendamentoAsync(Agendamento agendamento)
        //{
        //    _context.Agendamentos.Update(agendamento);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task PatchAgendamentoAsync(Agendamento agendamento)
        //{
        //    _context.Agendamentos.Update(agendamento);
        //    await _context.SaveChangesAsync();
        //}

        //public async Task DeleteAgendamentoAsync(Agendamento agendamento)
        //{
        //    _context.Agendamentos.Remove(agendamento);
        //    await _context.SaveChangesAsync();
        //}
    }
}
