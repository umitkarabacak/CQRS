namespace CQRS.ReadProduct.Application.Products.Queries.GetProductsListWithPagination
{
    public class ProductListItemDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double? UnitPrice { get; set; }
    }
}
