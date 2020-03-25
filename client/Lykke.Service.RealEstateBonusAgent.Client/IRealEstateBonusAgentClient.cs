using JetBrains.Annotations;

namespace Lykke.Service.RealEstateBonusAgent.Client
{
    /// <summary>
    /// RealEstateBonusAgent client interface.
    /// </summary>
    [PublicAPI]
    public interface IRealEstateBonusAgentClient
    {
        // Make your app's controller interfaces visible by adding corresponding properties here.
        // NO actual methods should be placed here (these go to controller interfaces, for example - IRealEstateBonusAgentApi).
        // ONLY properties for accessing controller interfaces are allowed.

        /// <summary>Application Api interface</summary>
        IRealEstateBonusAgentApi Api { get; }
    }
}
