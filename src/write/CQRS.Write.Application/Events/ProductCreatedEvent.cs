namespace CQRS.Write.Application.Events
{
    public class ProductCreatedEvent
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double? UnitPrice { get; set; }
        public int CategoryId { get; set; }
    }
}
