namespace UserContactRegistration.Infrastructure.PostgreSQL.Entities
{
    public class DocumentTypeDto
    {
        public long document_type_id { get; set; }
        public string code { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public DateTime created_at { get; set; } 
    }
}
