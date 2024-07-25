namespace Domain.Entities.Output
{
    //[SwaggerSchemaFilter(typeof(AgendamentoOutputDTOSchemaFilter))]
    public class AgendamentoOutput
    {
        public AgendamentoOutput(Agendamento agendamento)
        {
            IdAgendamento = agendamento.Id;
            IdPaciente = agendamento.IdPaciente;
            IdMedico = agendamento.IdMedico;
            DataAgendamento = agendamento.DataAgendamento;
            Motivo = agendamento.Motivo;
            StatusAgendamento = agendamento.StatusAgendamento;

        }

        public string IdAgendamento { get; set; }
        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string? Motivo { get; set; }
        public string StatusAgendamento { get; set; }
    }
}
