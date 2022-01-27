using MediatR;
using Nest;

namespace CQRS.ReadCategory.Application.Categories.Queries.GetCategoriesListWithPagination
{
    public class GetCategoryListQueryHandler : IRequestHandler<GetCategoryListQuery, PaginatedItemsViewModel<CategoryListItemDto>>
    {
        private readonly ElasticClient _elasticClient;

        public GetCategoryListQueryHandler(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<PaginatedItemsViewModel<CategoryListItemDto>> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
        {
            var searchResponse = await _elasticClient.SearchAsync<CategoryListItemDto>();

            return new PaginatedItemsViewModel<CategoryListItemDto>(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                count: searchResponse.Documents.Count,
                data: searchResponse.Documents
            );
        }
    }
}
