namespace CQRS.WriteProduct.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double? UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
