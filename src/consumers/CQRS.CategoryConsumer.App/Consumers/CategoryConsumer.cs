using Events;
using MassTransit;
using Nest;
using System.Text.Json;

namespace CQRS.CategoryConsumer.App.Consumers
{
    public class CategoryConsumer : IConsumer<CategoryCreatedEvent>
    {
        private readonly ILogger<CategoryConsumer> _logger;
        private readonly ElasticClient _elasticClient;

        public CategoryConsumer(ILogger<CategoryConsumer> logger
            , ElasticClient elasticClient)
        {
            _logger = logger;
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<CategoryCreatedEvent> context)
        {
            var consumerMessage = $"Consume category item {JsonSerializer.Serialize(context.Message)}";

            _logger.LogInformation(consumerMessage);

            string indexName = "categories";
            await ChekIndex(indexName);

            var createResponse = await _elasticClient.CreateAsync(context.Message, q => q.Index(indexName));
            if (createResponse.ApiCall?.HttpStatusCode == 409)
            {
                await _elasticClient.UpdateAsync<CategoryCreatedEvent>(createResponse.Id, a => a.Index(indexName).Doc(context.Message));
            }
        }


        private async Task ChekIndex(string indexName)
        {
            var existsResponse = await _elasticClient.Indices.ExistsAsync(indexName);
            if (existsResponse.Exists)
                return;

            await _elasticClient.Indices.CreateAsync(indexName, ci => ci.Index(indexName));
        }
    }
}
