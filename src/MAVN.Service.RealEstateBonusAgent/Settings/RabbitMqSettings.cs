using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.RealEstateBonusAgent.Settings
{
    public class RabbitMqSettings
    {
        [AmqpCheck]
        public string ConnectionString { get; set; }
    }
}
