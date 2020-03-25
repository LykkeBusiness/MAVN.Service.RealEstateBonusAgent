using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Entities
{
    [Table("deposit")]
    public class InstallmentEntity : BaseEntity
    {
        [Column("installment_number")]
        public int InstallmentNumber { get; set; }

        [Column("total_installments")]
        public int TotalInstallments { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("completion_amount")]
        public decimal CompletionAmount { get; set; }
        
        [Column("paid_amount")]
        public decimal PaidAmount { get; set; }

        [Column("net_property_price")]
        public decimal NetPropertyPrice { get; set; }

        [Column("referral_lead_id")]
        public string ReferralLeadId { get; set; }

        [Column("referrer_id")]
        public string ReferrerId { get; set; }

        [Column("customer_id")]
        public string CustomerId { get; set; }

        [Column("currency_code")]
        public string CurrencyCode { get; set; }
    }
}
