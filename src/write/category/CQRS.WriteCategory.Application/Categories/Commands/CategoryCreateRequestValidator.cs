namespace CQRS.WriteCategory.Application.Categories.Commands
{
    public class CategoryCreateRequestValidator : AbstractValidator<CategoryCreateRequest>
    {
        public CategoryCreateRequestValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Name field is required");
        }
    }
}
