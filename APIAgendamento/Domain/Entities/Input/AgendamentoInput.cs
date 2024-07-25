using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities.Input
{
    //[SwaggerSchemaFilter(typeof(AgendamentoInputSchemaFilter))]
    public class AgendamentoInput
    {
        [Required]
        public int IdPaciente { get; set; }

        [Required]
        public int IdMedico { get; set; }

        [Required]
        public DateTime DataAgendamento { get; set; }

        [Required]
        [StringLength(255)]
        public string? Motivo { get; set; }

        [Required]
        [StringLength(20)]
        public string? StatusAgendamento { get; set; }

    }
}
