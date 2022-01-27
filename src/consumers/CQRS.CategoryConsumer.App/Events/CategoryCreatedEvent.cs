namespace Events
{
    public class CategoryCreatedEvent
    {
        public Guid Id => Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
