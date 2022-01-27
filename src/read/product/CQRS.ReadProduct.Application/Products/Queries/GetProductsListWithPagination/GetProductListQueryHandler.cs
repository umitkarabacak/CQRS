using MediatR;
using Nest;

namespace CQRS.ReadProduct.Application.Products.Queries.GetProductsListWithPagination
{
    public class GetProductListQueryHandler : IRequestHandler<GetProductListQuery, PaginatedItemsViewModel<ProductListItemDto>>
    {
        private readonly ElasticClient _elasticClient;

        public GetProductListQueryHandler(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<PaginatedItemsViewModel<ProductListItemDto>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            var searchResponse = await _elasticClient.SearchAsync<ProductListItemDto>();

            return new PaginatedItemsViewModel<ProductListItemDto>(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                count: searchResponse.Documents.Count,
                data: searchResponse.Documents
            );
        }
    }
}
