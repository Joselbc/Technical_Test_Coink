namespace UserContactRegistration.Domain.Entities
{
    public class Address
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long CountryId { get; set; }
        public long DepartmentId { get; set; }
        public long MunicipalityId { get; set; }
        public string AddressValue { get; set; } = string.Empty;
        public string? Complement { get; set; }
        public string? PostalCode { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
