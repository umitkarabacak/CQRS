using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace CQRS.WriteCategory.Application.Categories.Commands
{
    public class CategoryCreateRequestHandler : IRequestHandler<CategoryCreateRequest>
    {
        private readonly ILogger<CategoryCreateRequestHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IProjectContext _projectContext;

        public CategoryCreateRequestHandler(ILogger<CategoryCreateRequestHandler> logger
            , IMapper mapper
            , IProjectContext projectContext)
        {
            _logger = logger;
            _mapper = mapper;
            _projectContext = projectContext;
        }

        public async Task<Unit> Handle(CategoryCreateRequest request, CancellationToken cancellationToken)
        {
            var category = _mapper.Map<Category>(request);
            await _projectContext.Categories.AddAsync(category, cancellationToken);
            await _projectContext.SaveChangesAsync(cancellationToken);

            var categoryCreatedEvent = _mapper.Map<CategoryCreatedEvent>(category);

            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOn = DateTime.UtcNow,
                EventType = typeof(CategoryCreatedEvent).ToString(),
                Payload = JsonSerializer.Serialize(categoryCreatedEvent)
            };
            await _projectContext.OutboxMessages.AddAsync(outboxMessage, cancellationToken);

            await _projectContext.SaveChangesAsync(cancellationToken);


            return Unit.Value;
        }
    }
}
