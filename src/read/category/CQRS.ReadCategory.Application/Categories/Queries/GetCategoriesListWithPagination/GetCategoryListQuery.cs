using MediatR;

namespace CQRS.ReadCategory.Application.Categories.Queries.GetCategoriesListWithPagination
{
    public class GetCategoryListQuery : IRequest<PaginatedItemsViewModel<CategoryListItemDto>>
    {
        public int PageIndex { get; init; }

        public int PageSize { get; init; }

        public GetCategoryListQuery(int pageSize, int pageIndex)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
