namespace UserContactRegistration.Domain.Entities
{
    public class Phone
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public string? PhoneType { get; set; }
        public string PhoneValue { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
