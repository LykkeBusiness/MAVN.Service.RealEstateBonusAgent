using JetBrains.Annotations;
using Lykke.SettingsReader.Attributes;

namespace MAVN.Service.RealEstateBonusAgent.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class RealEstateBonusAgentSettings
    {
        public DbSettings Db { get; set; }

        public RabbitMqSettings Rabbit { get; set; }
    }
}
