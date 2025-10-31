namespace UserContactRegistration.Infrastructure.PostgreSQL.Entities
{
    public class DepartmentDto
    {
        public long department_id { get; set; }
        public long country_id { get; set; }
        public string? code { get; set; }
        public string name { get; set; } = string.Empty;
        public DateTime created_at { get; set; }
    }
}
