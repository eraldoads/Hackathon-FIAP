using Domain.Entities.Input;

namespace Application.Interfaces
{
    public class DateTimeAdjuster
    {
        public static DateTime AjustaDataHoraLocal(AgendamentoInput AgendamentoInput)
        {
            // obtém o fuso horário do servidor
            var fusoServidor = TimeZoneInfo.Local;
            // obtém o fuso horário brasileiro pelo seu identificador
            var fusoBrasil = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            // converte a data do pedido para o fuso horário brasileiro
            var dataPedidoAjustada = TimeZoneInfo.ConvertTime(AgendamentoInput.DataAgendamento, fusoServidor, fusoBrasil);
            return dataPedidoAjustada;
        }
    }
}