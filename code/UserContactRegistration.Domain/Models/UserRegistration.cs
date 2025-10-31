namespace UserContactRegistration.Domain.Models
{
    public class UserRegistration
    {
        public string DocumentNumber { get; set; } = string.Empty;
        public long DocumentTypeId { get; set; } 
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneValue { get; set; } = string.Empty;
        public string PhoneType { get; set; } = "mobile";
        public long CountryId { get; set; }
        public long DepartmentId { get; set; }
        public long MunicipalityId { get; set; }
        public string AddressValue { get; set; } = string.Empty;
        public string? Complement { get; set; }
        public string? PostalCode { get; set; }
    }
}
