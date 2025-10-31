namespace UserContactRegistration.Domain.Entities
{
    public class Country
    {
        public long Id { get; init; }
        public string IsoCode { get; init; } = string.Empty;
        public string Name { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }

    }
}
