using CQRS.Distributor.App.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CQRS.Distributor.App.BackgroundServices
{
    public class OutboxMessageService : IHostedService
    {
        private readonly ILogger<OutboxMessageService> _logger;
        private readonly ProjectContext _projectContext;
        private readonly IBus _bus;

        public OutboxMessageService(ILogger<OutboxMessageService> logger
            , IServiceScopeFactory serviceScopeFactoryfactory
            , IBus bus)
        {
            _logger = logger;
            _projectContext = serviceScopeFactoryfactory.CreateScope().ServiceProvider.GetRequiredService<ProjectContext>();
            _bus = bus;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Started DistributorApp");

            while (!cancellationToken.IsCancellationRequested)
            {
                var outBoxMessages = await _projectContext.OutboxMessages
                    .Where(om => !om.IsPublished
                        && !om.PublishedDate.HasValue
                    )
                    .ToListAsync(cancellationToken);

                foreach (var outboxMessage in outBoxMessages)
                {
                    Type type = Assembly.GetAssembly(typeof(Program)).GetType(outboxMessage.EventType);
                    if (type is null)
                        return;

                    object eventBusData = System.Text.Json.JsonSerializer.Deserialize(outboxMessage.Payload, type);

                    if (eventBusData is null)
                        return;

                    await _bus.Publish(eventBusData, cancellationToken);

                    outboxMessage.PublishedDate = DateTime.UtcNow;
                    outboxMessage.IsPublished = true;

                    _projectContext.Update(outboxMessage).State = EntityState.Modified;
                }

                await _projectContext.SaveChangesAsync(cancellationToken);

                await Task.Delay(1000);
            }
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogError($"Stoped DistributorApp");

            await Task.CompletedTask;
        }
    }
}
