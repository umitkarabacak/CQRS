namespace CQRS.WriteCategory.Application.Categories.Commands
{
    public class CategoryCreateRequest : IRequest, IMapFrom<Category>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CategoryCreateRequest, Category>();
            profile.CreateMap<Category, CategoryCreatedEvent>();
        }
    }
}
