using MassTransit;

namespace CQRS.ProductConsumer.App.Services
{
    public class ConsumeProductService : IHostedService
    {
        private readonly ILogger<ConsumeProductService> _logger;
        private readonly IBusControl _busControl;

        public ConsumeProductService(ILogger<ConsumeProductService> logger
            , IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer product background service started.");

            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer product background service stopped!");

            await _busControl.StopAsync(cancellationToken);
        }
    }
}
