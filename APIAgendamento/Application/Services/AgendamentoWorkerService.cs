using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class AgendamentoWorkerService : BackgroundService
    {
        private readonly ILogger<AgendamentoWorkerService> _logger;
        public IServiceProvider Services { get; }

        public AgendamentoWorkerService(IServiceProvider services, ILogger<AgendamentoWorkerService> logger)
        {
            Services = services;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await DoWork(stoppingToken);
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            using (var scope = Services.CreateScope())
            {
                IAgendamentoScopedService scopedProcessingService = scope.ServiceProvider.GetRequiredService<IAgendamentoScopedService>();
                await scopedProcessingService.DoWork(cancellationToken);
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await base.StopAsync(cancellationToken);
        }
    }
}
