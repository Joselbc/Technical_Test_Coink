namespace UserContactRegistration.Infrastructure.PostgreSQL.Settings
{
    public class PostgreSettings
    {
        public string? Host { get; set; }
        public string? Port { get; set; }
        public string? Db { get; set; }
        public string? DbUser { get; set; }
        public string? DbPassword { get; set; }

        public string? GetConnectionString()
        {
            return $"Host={Host};Port={Port};Database={Db};Username={DbUser};Password={DbPassword};";
        }
    }
}
