using Lykke.HttpClientGenerator;

namespace Lykke.Service.RealEstateBonusAgent.Client
{
    /// <summary>
    /// RealEstateBonusAgent API aggregating interface.
    /// </summary>
    public class RealEstateBonusAgentClient : IRealEstateBonusAgentClient
    {
        // Note: Add similar Api properties for each new service controller

        /// <summary>Inerface to RealEstateBonusAgent Api.</summary>
        public IRealEstateBonusAgentApi Api { get; private set; }

        /// <summary>C-tor</summary>
        public RealEstateBonusAgentClient(IHttpClientGenerator httpClientGenerator)
        {
            Api = httpClientGenerator.Generate<IRealEstateBonusAgentApi>();
        }
    }
}
