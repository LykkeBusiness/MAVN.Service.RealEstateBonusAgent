using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.RealEstateBonusAgent.Settings
{
    public class DbSettings
    {
        [AzureTableCheck]
        public string LogsConnString { get; set; }

        public string MsSqlConnectionString { get; set; }
    }
}
