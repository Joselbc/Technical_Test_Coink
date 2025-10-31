namespace UserContactRegistration.Infrastructure.PostgreSQL.Entities
{
    public class AddressDto
    {
        public long address_id { get; set; }
        public long user_id { get; set; }
        public long country_id { get; set; }
        public long department_id { get; set; }
        public long municipality_id { get; set; }
        public string address { get; set; } = string.Empty;
        public string? complement { get; set; }
        public string? postal_code { get; set; }
        public DateTime created_at { get; set; }
    }
}
