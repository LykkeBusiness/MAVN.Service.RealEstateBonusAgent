using System;

namespace Lykke.Service.RealEstateBonusAgent.Domain.Model
{
    public class DownPaymentModel
    {
        public Guid Id { get; set; }

        public string ReferralLeadId { get; set; }

        public DateTime Timestamp { get; set; }

        public string CurrencyCode { get; set; }

        public bool IsFullReward { get; set; }

        public decimal DownPaymentPercentage { get; set; }

        public decimal FirstInstallmentAmount { get; set; }

        public decimal CalculatedCommissionAmount { get; set; }
        
        public decimal NetPropertyPrice { get; set; }

        public string ReferrerId { get; set; }

        public string CustomerId { get; set; }

        public string UnitLocationCode { get; set; }
    }
}
