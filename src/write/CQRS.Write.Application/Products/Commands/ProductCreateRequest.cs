using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CQRS.Write.Application.Products.Commands
{
    public class ProductCreateRequest : IRequest, IMapFrom<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? UnitPrice { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductCreateRequest, Product>();
            profile.CreateMap<ProductCreateRequest, ProductCreatedEvent>();
        }
    }

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


            var productCreatedEvent = _mapper.Map<ProductCreatedEvent>(request);
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

    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Name field is required");
        }
    }
}
