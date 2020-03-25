using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.RealEstateBonusAgent.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }
    }
}
