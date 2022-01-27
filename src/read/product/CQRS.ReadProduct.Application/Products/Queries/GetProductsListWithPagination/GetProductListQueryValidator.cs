using FluentValidation;

namespace CQRS.ReadProduct.Application.Products.Queries.GetProductsListWithPagination
{
    public class GetProductListQueryValidator : AbstractValidator<GetProductListQuery>
    {
        private static readonly int minValue = 0;
        private static readonly int maxPageSize = 100;

        public GetProductListQueryValidator()
        {
            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(minValue)
                .WithMessage($"Page index min value greather than {minValue}");

            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(minValue)
                .WithMessage($"Page size min value greather than {minValue}")
                .LessThanOrEqualTo(maxPageSize)
                .WithMessage($"Page size maxvalue less than {maxPageSize}");
        }
    }
}
