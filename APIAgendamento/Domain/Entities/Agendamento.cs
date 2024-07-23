using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    //[SwaggerSchemaFilter(typeof(AgendamentoSchemaFilter))]
    public class Agendamento
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public int IdPaciente { get; set; }
        public int IdMedico { get; set; }
        public DateTime DataAgendamento { get; set; }
        public string? Motivo { get; set; }
    }

}