namespace CQRS.Write.Application.Products.Commands
{
    public class ProductCreateRequest : IRequest, IMapFrom<Product>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? UnitPrice { get; set; }
        public int CategoryId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ProductCreateRequest, Product>();
            profile.CreateMap<ProductCreateRequest, ProductCreatedEvent>();
        }
    }
}
