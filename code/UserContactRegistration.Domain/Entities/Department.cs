namespace UserContactRegistration.Domain.Entities
{
    public class Department
    {
        public long Id { get; set; }
        public long CountryId { get; set; }
        public string? Code { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
