using MassTransit;

namespace CQRS.CategoryConsumer.App.Services
{
    public class ConsumeCategoryService : IHostedService
    {
        private readonly ILogger<ConsumeCategoryService> _logger;
        private readonly IBusControl _busControl;

        public ConsumeCategoryService(ILogger<ConsumeCategoryService> logger
            , IBusControl busControl)
        {
            _logger = logger;
            _busControl = busControl;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer category background service started.");

            await _busControl.StartAsync(cancellationToken);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Consumer category background service stopped!");

            await _busControl.StopAsync(cancellationToken);
        }
    }
}
