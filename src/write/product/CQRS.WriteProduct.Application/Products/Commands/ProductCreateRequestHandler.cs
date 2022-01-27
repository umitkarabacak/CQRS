using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CQRS.WriteProduct.Application.Products.Commands
{
    public class ProductCreateRequestHandler : IRequestHandler<ProductCreateRequest>
    {
        private readonly ILogger<ProductCreateRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IProjectContext _projectContext;

        public ProductCreateRequestHandler(ILogger<ProductCreateRequestHandler> logger
            , IMapper mapper
            , IProjectContext projectContext)
        {
            _logger = logger;
            _mapper = mapper;
            _projectContext = projectContext;
        }

        public async Task<Unit> Handle(ProductCreateRequest request, CancellationToken cancellationToken)
        {
            var product = _mapper.Map<Product>(request);
            await _projectContext.Products.AddAsync(product, cancellationToken);
            await _projectContext.SaveChangesAsync(cancellationToken);

            var productCreatedEvent = _mapper.Map<ProductCreatedEvent>(product);
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.UtcNow,
                EventType = typeof(ProductCreatedEvent).ToString(),
                Payload = JsonSerializer.Serialize(productCreatedEvent)
            };
            await _projectContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

            await _projectContext.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}
