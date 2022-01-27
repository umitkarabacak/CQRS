using Events;
using MassTransit;
using System.Text.Json;

namespace CQRS.CategoryConsumer.App.Consumers
{
    public class CategoryConsumer : IConsumer<CategoryCreatedEvent>
    {
        private readonly ILogger<CategoryConsumer> _logger;

        public CategoryConsumer(ILogger<CategoryConsumer> logger)
        {
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CategoryCreatedEvent> context)
        {
            var consumerMessage = $"Consume category item {JsonSerializer.Serialize(context.Message)}";

            _logger.LogInformation(consumerMessage);

            // TODO: write the message to elastic search
            await Task.CompletedTask;
        }
    }
}
