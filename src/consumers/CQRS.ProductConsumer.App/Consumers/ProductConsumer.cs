using Events;
using MassTransit;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CQRS.ProductConsumer.App.Consumers
{
    public class ProductConsumer : IConsumer<ProductCreatedEvent>
    {
        private readonly ILogger<ProductConsumer> _logger;
        private readonly ElasticClient _elasticClient;

        public ProductConsumer(ILogger<ProductConsumer> logger
            , ElasticClient elasticClient)
        {
            _logger = logger;
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<ProductCreatedEvent> context)
        {
            var consumerMessage = $"Consume product item {JsonSerializer.Serialize(context.Message)}";

            _logger.LogInformation(consumerMessage);

            string indexName = "products";
            await ChekIndex(indexName);

            var createResponse = await _elasticClient.CreateAsync(context.Message, q => q.Index(indexName));
            if (createResponse.ApiCall?.HttpStatusCode == 409)
            {
                await _elasticClient.UpdateAsync<ProductCreatedEvent>(createResponse.Id, a => a.Index(indexName).Doc(context.Message));
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
