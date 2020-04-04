using System;

namespace MAVN.Service.RealEstateBonusAgent.Domain.Model
{
    public class InstallmentModel
    {
        public Guid Id { get; set; }

        public int InstallmentNumber { get; set; }

        public int TotalInstallments { get; set; }

        public DateTime Timestamp { get; set; }

        public decimal PaidAmount { get; set; }

        public decimal CompletionAmount { get; set; }

        public decimal NetPropertyPrice { get; set; }

        public string ReferralLeadId { get; set; }

        public string ReferrerId { get; set; }

        public string CustomerId { get; set; }

        public string CurrencyCode { get; set; }

        public string UnitLocationCode { get; set; }
    }
}
