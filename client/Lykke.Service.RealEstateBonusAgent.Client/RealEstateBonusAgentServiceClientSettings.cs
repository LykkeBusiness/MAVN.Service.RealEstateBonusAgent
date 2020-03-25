﻿using Lykke.SettingsReader.Attributes;

namespace Lykke.Service.RealEstateBonusAgent.Client 
{
    /// <summary>
    /// RealEstateBonusAgent client settings.
    /// </summary>
    public class RealEstateBonusAgentServiceClientSettings 
    {
        /// <summary>Service url.</summary>
        [HttpCheck("api/isalive")]
        public string ServiceUrl {get; set;}
    }
}
