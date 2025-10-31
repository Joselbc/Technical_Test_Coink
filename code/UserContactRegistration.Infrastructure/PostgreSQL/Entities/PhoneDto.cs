namespace UserContactRegistration.Infrastructure.PostgreSQL.Entities
{
    public class PhoneDto
    {
        public long phone_id { get; set; }
        public long user_id { get; set; }
        public string? phone_type { get; set; }
        public string phone_value { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
    }
}
