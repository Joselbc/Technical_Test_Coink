namespace UserContactRegistration.Infrastructure.PostgreSQL.Entities
{
    public class UserDto
    {
        public long user_id { get; set; }
        public int document_type_id { get; set; }
        public string document_number { get; set; } = string.Empty;
        public string first_name { get; set; } = string.Empty;
        public string? last_name { get; set; }
        public string? email { get; set; }
        public DateTime created_at { get; set; }
    }
}
