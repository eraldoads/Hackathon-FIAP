using Application.Interfaces;
using Domain.Entities.Input;
using Domain.Entities.Output;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json", [])]
    [SwaggerResponse(204, "Requisição concluída sem dados de retorno.", null)]
    [SwaggerResponse(400, "A solicitação não pode ser entendida pelo servidor devido à sintaxe malformada.", null)]
    [SwaggerResponse(401, "A requisição requer autenticação do usuário.", null)]
    [SwaggerResponse(403, "Privilégios insuficientes.", null)]
    [SwaggerResponse(404, "O recurso solicitado não existe.", null)]
    [SwaggerResponse(412, "Condição prévia dada em um ou mais dos campos avaliada como falsa.", null)]
    [SwaggerResponse(500, "O servidor encontrou uma condição inesperada.", null)]
    [Consumes("application/json", [])]

    public class AgendamentoController(IAgendamentoService agendamentoService) : ControllerBase
    {
        private readonly IAgendamentoService _agendamentoService = agendamentoService;

        // GET : /agendamento
        [HttpGet]
        [SwaggerOperation(
            Summary = "Endpoint para retornar todos os agendamentos realizados",
            Description = "Busca todos os agendamentos realizados",
            Tags = ["Agendamento"]
        )]
        [SwaggerResponse(200, "Consulta executada com sucesso!", typeof(List<AgendamentoOutput>))]
        [SwaggerResponse(206, "Conteúdo Parcial!", typeof(List<AgendamentoOutput>))]
        public async Task<ActionResult<IEnumerable<AgendamentoOutput>>> GetAgendamento()
        {
            var agendamentos = await _agendamentoService.GetAgendamentoAsync();
            return Ok(agendamentos);
        }

        // GET : /agendamento/{id}
        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Endpoint para retornar um agendamento pelo ID",
            Description = @"Busca um agendamento específico pelo ID</br>
                             <b>Parâmetros de entrada:</b>
                             <br/> • <b>id</b>: o identificador do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                             ",
            Tags = ["Agendamento"]
        )]
        [SwaggerResponse(200, "Consulta executada com sucesso!", typeof(AgendamentoOutput))]
        [SwaggerResponse(404, "O agendamento não foi encontrado.", null)]
        public async Task<ActionResult<AgendamentoOutput>> GetAgendamentoById(string id)
        {
            var agendamento = await _agendamentoService.GetAgendamentoByIdAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            return Ok(agendamento);
        }

        // POST : /agendamento
        [HttpPost]
        [SwaggerOperation(
            Summary = "Endpoint para criar um novo agendamento",
            Description = @"Cria um novo agendamento com os dados recebidos no corpo da requisição </br>
                            </br>
                            <b>Parâmetros de entrada:</b>
                            <br/> • <b>idPaciente</b>: o identificador do paciente que está agendando ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>idMedico</b>: o identificador do médico que está agendando ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>dataAgendamento</b>: a data e hora do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>motivo</b>: o motivo do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>statusAgendamento</b>: o status do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/><br/>&nbsp;&nbsp;&nbsp;<b>Possíveis Status da Consulta:</b>
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Solicitado
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Confirmado
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Cancelado
                        ",
            Tags = ["Agendamento"]
        )]
        [SwaggerResponse(201, "Agendamento criado com sucesso!", typeof(AgendamentoOutput))]
        [SwaggerResponse(400, "A solicitação não pode ser entendida pelo servidor devido à sintaxe malformada.", null)]
        public async Task<ActionResult<AgendamentoOutput>> PostAgendamento([FromBody] AgendamentoInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agendamento = await _agendamentoService.PostAgendamentoAsync(input);
            return CreatedAtAction(nameof(GetAgendamentoById), new { id = agendamento.IdAgendamento }, agendamento);
        }

        // PUT : /agendamento/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Endpoint para atualizar um agendamento",
            Description = @"Atualiza todos os campos de um agendamento com os dados recebidos no corpo da requisição </br>
                            </br>                             
                            <b>Parâmetros de entrada:</b>
                            <br/> • <b>id</b>: o identificador do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>idPaciente</b>: o identificador do paciente que está agendando ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>idMedico</b>: o identificador do médico que está agendando ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>dataAgendamento</b>: a data e hora do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>motivo</b>: o motivo do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>statusAgendamento</b>: o status do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/><br/>&nbsp;&nbsp;&nbsp;<b>Possíveis Status da Consulta:</b>
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Solicitado
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Confirmado
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Cancelado
                            ",
            Tags = ["Agendamento"]
        )]
        [SwaggerResponse(200, "Agendamento atualizado com sucesso!", typeof(AgendamentoOutput))]
        [SwaggerResponse(400, "A solicitação não pode ser entendida pelo servidor devido à sintaxe malformada.", null)]
        [SwaggerResponse(404, "O agendamento não foi encontrado.", null)]
        public async Task<ActionResult<AgendamentoOutput>> PutAgendamento(string id, [FromBody] AgendamentoInput input)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var agendamento = await _agendamentoService.PutAgendamentoAsync(id, input);
            if (agendamento == null)
            {
                return NotFound();
            }
            return Ok(agendamento);
        }

        // PATCH : /agendamento/{id}
        [HttpPatch("{id}")]
        [SwaggerOperation(
            Summary = "Endpoint para atualizar parcialmente um agendamento",
            Description = @"Atualiza campos específicos de um agendamento com os dados recebidos no corpo da requisição </br>
                            </br>
                            <b>Parâmetros de entrada:</b>
                            <br/> • <b>idPaciente</b>: o identificador do paciente que está agendando ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>idMedico</b>: o identificador do médico que está agendando ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>dataAgendamento</b>: a data e hora do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>motivo</b>: o motivo do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/> • <b>statusAgendamento</b>: o status do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                            <br/><br/>&nbsp;&nbsp;&nbsp;<b>Possíveis Status da Consulta:</b>
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Solicitado
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Confirmado
                            <br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <b> • </b> Cancelado
                            <br/><b>Exemplo de corpo da requisição:</b>
                            <pre>
                               &nbsp;&nbsp;[
                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;""op"": ""replace"",
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;""path"": ""/dataAgendamento"",
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;""value"": ""2024-07-23T02:01:07.904Z""
                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;},
                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;""op"": ""replace"",
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;""path"": ""/motivo"",
                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;""value"": ""Consulta de rotina""
                                   &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;}
                               &nbsp;&nbsp;]
                            </pre>
                            ",
            Tags = ["Agendamento"]
        )]
        [SwaggerResponse(200, "Agendamento atualizado com sucesso!", typeof(AgendamentoOutput))]
        [SwaggerResponse(400, "A solicitação não pode ser entendida pelo servidor devido à sintaxe malformada.", null)]
        [SwaggerResponse(404, "O agendamento não foi encontrado.", null)]
        public async Task<ActionResult<AgendamentoOutput>> PatchAgendamento(string id, [FromBody] JsonPatchDocument<AgendamentoInput> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var agendamento = await _agendamentoService.GetAgendamentoByIdAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            var agendamentoToPatch = new AgendamentoInput
            {
                IdPaciente = agendamento.IdPaciente,
                IdMedico = agendamento.IdMedico,
                DataAgendamento = agendamento.DataAgendamento,
                Motivo = agendamento.Motivo
            };

            patchDoc.ApplyTo(agendamentoToPatch, ModelState);

            if (!TryValidateModel(agendamentoToPatch))
            {
                return ValidationProblem(ModelState);
            }

            var updatedAgendamento = await _agendamentoService.PatchAgendamentoAsync(id, agendamentoToPatch);
            return Ok(updatedAgendamento);

        }

        // DELETE : /agendamento/{id}
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Endpoint para deletar um agendamento",
            Description = @"Deleta um agendamento específico pelo ID</br>
                             <b>Parâmetros de entrada:</b>
                             <br/> • <b>id</b>: o identificador do agendamento ⇒ <font color='red'><b>Obrigatório</b></font>
                             ",
            Tags = ["Agendamento"]
        )]
        [SwaggerResponse(204, "Agendamento deletado com sucesso!", null)]
        [SwaggerResponse(404, "O agendamento não foi encontrado.", null)]
        public async Task<IActionResult> DeleteAgendamento(string id)
        {
            var agendamento = await _agendamentoService.GetAgendamentoByIdAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }

            await _agendamentoService.DeleteAgendamentoAsync(id);
            return NoContent();
        }
    }
}
