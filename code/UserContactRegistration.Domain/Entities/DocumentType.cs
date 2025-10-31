namespace UserContactRegistration.Domain.Entities
{
    public class DocumentType
    {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
