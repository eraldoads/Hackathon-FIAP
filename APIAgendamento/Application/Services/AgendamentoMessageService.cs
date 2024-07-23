namespace Application.Services
{
    //public class AgendamentoMessageService : IAgendamentoMessageService
    //{
    //    private readonly IAgendamentoMessageQueue _AgendamentoMessageQueue;
    //    private readonly IAgendamentoMessageQueueError _AgendamentoMessageQueueError;
    //    private readonly IAgendamentoService _AgendamentoService;
    //    private readonly ILogger<AgendamentoMessageService> _logger;

    //    public AgendamentoMessageService(IAgendamentoMessageQueue AgendamentoMessageQueue,
    //                                     IAgendamentoMessageQueueError AgendamentoMessageQueueError,
    //                                     IAgendamentoService pagamentoService,
    //                                     ILogger<AgendamentoMessageService> logger) 
    //    { 
    //        _AgendamentoMessageQueue = AgendamentoMessageQueue;
    //        _AgendamentoMessageQueueError = AgendamentoMessageQueueError;
    //        _AgendamentoService = pagamentoService;
    //        _logger = logger;

    //        _AgendamentoMessageQueue.MessageReceived += ReceberMensagemAsync;
    //        _AgendamentoMessageQueueError.MessageReceived += ReceberMensagemAsyncError;
    //    }

    //    public async Task ReceberMensagens()
    //    {
    //        await _AgendamentoMessageQueue.StartListening();
    //        await _AgendamentoMessageQueueError.StartListening();
    //    }

    //    private async Task ReceberMensagemAsync(string mensagem)
    //    {
    //        try
    //        {
    //            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    //            {                    
    //                PagamentoInput pagamentoInput = JsonSerializer.Deserialize<PagamentoInput>(mensagem);

    //                if (pagamentoInput.statusPagamento.Equals("Aprovado"))
    //                {
    //                    await _AgendamentoService.UpdateStatusAgendamento(pagamentoInput.idPedido, "Em Preparação");
    //                }

    //                scope.Complete();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Erro ao processar mensagem recebida.");
    //            throw; // Rethrow exception para garantir que a mensagem é reinfileirada via PedidoMessageQueue
    //        }
    //    }

    //    private async Task ReceberMensagemAsyncError(string mensagem)
    //    {
    //        try
    //        {
    //            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
    //            {
    //                AgendamentoDTO agendamento = JsonSerializer.Deserialize<AgendamentoDTO>(mensagem);

    //                await _AgendamentoService.UpdateStatusAgendamento(agendamento.IdAgendamento, "Cancelado");

    //                scope.Complete();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            _logger.LogError(ex, "Erro ao processar mensagem recebida.");
    //            throw; // Rethrow exception para garantir que a mensagem é reinfileirada via AgendamentoMessageQueueError
    //        }
    //    }

    //    public void Dispose()
    //    {
    //        _AgendamentoMessageQueue.MessageReceived -= ReceberMensagemAsync;
    //        _AgendamentoMessageQueueError.MessageReceived -= ReceberMensagemAsyncError;
    //        GC.SuppressFinalize(this);
    //    }
    //}
}
