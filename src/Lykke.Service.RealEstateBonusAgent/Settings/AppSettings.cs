using JetBrains.Annotations;
using Lykke.Sdk.Settings;
using Lykke.Service.Referral.Client;

namespace Lykke.Service.RealEstateBonusAgent.Settings
{
    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class AppSettings : BaseAppSettings
    {
        public RealEstateBonusAgentSettings RealEstateBonusAgentService { get; set; }

        public ReferralServiceClientSettings ReferralServiceClient { get; set; }
    }
}
