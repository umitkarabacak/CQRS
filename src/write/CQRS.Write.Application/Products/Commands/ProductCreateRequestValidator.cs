namespace CQRS.Write.Application.Products.Commands
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequest>
    {
        public ProductCreateRequestValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .WithMessage("Name field is required");

            RuleFor(p => p.CategoryId)
                .NotEmpty()
                .GreaterThan(0)
                .WithMessage("Category Id field must greater than 0");
        }
    }
}
