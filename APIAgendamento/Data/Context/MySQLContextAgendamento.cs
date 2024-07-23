using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Context
{
    public class MySQLContextAgendamento : DbContext
    {
        public MySQLContextAgendamento(DbContextOptions<MySQLContextAgendamento> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("ConnectionMysql", ServerVersion.AutoDetect("ConnectionMysql"),
                    b => b.MigrationsAssembly("APIAgendamento")); // Especificando o assembly de migrações
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Agendamento>().HasKey(p => p.Id);
        }

        public DbSet<Agendamento> Agendamentos { get; set; }

    }
}