using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace Services
{
    public static class ConfigurationService
    {
        public static IConfiguration Configuration { get; private set; }
        public static void Init()
        {
            if (DbProviderFactories.GetFactory("System.Data.SqlClient") == null)
            {
                DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            }

            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();
            }
        }
    }
}
