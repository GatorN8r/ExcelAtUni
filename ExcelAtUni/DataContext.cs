namespace ExcelAtUni
{
    public class DataContext
    {
        private ConfigurationBuilder _configurationBuilder;
        public DataContext(ConfigurationBuilder configurationBuilder)
        {
            _configurationBuilder = configurationBuilder;
        }

        public string GetConnection()
        {
            string connectionString;
            _configurationBuilder.AddJsonFile("AppSettings.json");
            IConfiguration configuration = _configurationBuilder.Build();
            connectionString = configuration.GetConnectionString("Connection");
            return connectionString;
        }

        public string GetToken()
        {
            string appsToken;
            IConfiguration configuration = _configurationBuilder.Build();
            appsToken = configuration.GetSection("AppSettings:Token").Value;
            return appsToken;
        }
    }
}
