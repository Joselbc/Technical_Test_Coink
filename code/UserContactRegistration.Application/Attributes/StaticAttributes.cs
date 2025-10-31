namespace UserContactRegistration.Application.Attributes
{
    public class StaticAttributes
    {
        public static object GetPostgrelAttributes(ConfigurationManager config)
        {
            string? host = Environment.GetEnvironmentVariable("Host");
            string? port = Environment.GetEnvironmentVariable("Port");
            string? db = Environment.GetEnvironmentVariable("Db");
            string? dbUser = Environment.GetEnvironmentVariable("DbUser");
            string? dbPassword = Environment.GetEnvironmentVariable("DbPassword");

            if (host == null || port == null || db == null || dbUser == null || dbPassword == null)
            {
                return config.GetSection("PostgreSettings");
            }

            var myConfiguration = new Dictionary<string, string>
            {
                {"Host", host},
                {"Port", port},
                {"Db", db},
                {"DbUser", dbUser},
                {"DbPassword", dbPassword}
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(myConfiguration).Build();
            return configuration;
        }

    }
}
