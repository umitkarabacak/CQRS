using MediatR;

namespace CQRS.ReadProduct.Application.Products.Queries.GetProductsListWithPagination
{
    public class GetProductListQuery : IRequest<PaginatedItemsViewModel<ProductListItemDto>>
    {
        public int PageIndex { get; init; }

        public int PageSize { get; init; }

        public GetProductListQuery(int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
