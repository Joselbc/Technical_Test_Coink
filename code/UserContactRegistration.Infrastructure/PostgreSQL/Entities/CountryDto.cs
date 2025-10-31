namespace UserContactRegistration.Infrastructure.PostgreSQL.Entities
{
    public class CountryDto
    {
        public long country_id { get; set; }
        public string iso_code { get; set; } = string.Empty;
        public string name { get; set; } = string.Empty;
        public DateTime? created_at { get; set; }
    }
}
