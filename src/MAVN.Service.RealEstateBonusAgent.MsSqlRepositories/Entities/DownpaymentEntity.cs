using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAVN.Service.RealEstateBonusAgent.MsSqlRepositories.Entities
{
    [Table("down_payment")]
    public class DownPaymentEntity: BaseEntity
    {
        [Column("referrer_id")]
        public Guid ReferrerId { get; set; }

        [Column("referral_lead_id")]
        public string ReferralLeadId { get; set; }

        [Column("timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("currency_code")]
        public string CurrencyCode { get; set; }

        [Column("is_full_reward")]
        public bool IsFullReward { get; set; }

        [Column("down_payment_percentage")]
        public decimal DownPaymentPercentage { get; set; }

        [Column("first_installment_amount")]
        public decimal FirstInstallmentAmount { get; set; }

        [Column("calculated_commission_amount")]
        public decimal CalculatedCommissionAmount { get; set; }

        [Column("net_property_price")]
        public decimal NetPropertyPrice { get; set; }

        [Column("customer_id")]
        public string CustomerId { get; set; }
    }
}
