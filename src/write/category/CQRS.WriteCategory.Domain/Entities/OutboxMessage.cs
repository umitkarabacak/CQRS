namespace CQRS.WriteCategory.Domain.Entities
{
    public class OutboxMessage
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
        public string EventType { get; set; }
        public string Payload { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
    }
}
