using Microsoft.Extensions.Configuration;
using System.Data.Common;
using System.Data.SqlClient;

namespace Services
{
    public class ConfigurationService
    {
        public static IConfiguration Configuration { get; private set; }
        public static void Init()
        {
            // Почему то у меня не работает когда написана данная проверка...
            // Еще перенес этот сервис в Data, потому что конфликтуют зависимости

            //if (DbProviderFactories.GetFactory("System.Data.SqlClient") == null)
            //{
            //    DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            //}
            DbProviderFactories.RegisterFactory("System.Data.SqlClient", SqlClientFactory.Instance);
            if (Configuration == null)
            {
                var configurationBuilder = new ConfigurationBuilder();
                Configuration = configurationBuilder.AddJsonFile("appSettings.json").Build();
            }
        }
    }
}
