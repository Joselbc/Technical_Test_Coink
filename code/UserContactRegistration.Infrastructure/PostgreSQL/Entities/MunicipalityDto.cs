namespace UserContactRegistration.Infrastructure.PostgreSQL.Entities
{
    public class MunicipalityDto
    {
        public long municipality_id { get; set; }
        public long department_id { get; set; }
        public string name { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
    }
}
