using Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AgendamentoScopedService : IAgendamentoScopedService
    {
        private readonly IAgendamentoMessageService _AgendamentoMessageService;
        private readonly ILogger<AgendamentoScopedService> _logger;

        public AgendamentoScopedService(IAgendamentoMessageService pedidoMessageService, ILogger<AgendamentoScopedService> logger)
        {
            _AgendamentoMessageService = pedidoMessageService;
            _logger = logger;
        }

        public async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Serviço de escopo de pedido iniciado.");

            await _AgendamentoMessageService.ReceberMensagens();

            while (!cancellationToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(1000, cancellationToken); // Aguarda 1 segundo antes de tentar receber outra mensagem
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Erro ao receber mensagem.");
                }
            }

            _logger.LogInformation("Serviço de escopo de pedido encerrado.");
        }
    }
}
