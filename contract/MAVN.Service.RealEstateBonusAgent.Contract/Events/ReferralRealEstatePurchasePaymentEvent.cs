using System;

namespace MAVN.Service.RealEstateBonusAgent.Contract.Events
{
    /// <summary>
    /// An event fired when purchase is made towards a property purchase
    /// </summary>
    public class ReferralRealEstatePurchasePaymentEvent
    {
        /// <summary>
        /// The Id of the customer
        /// </summary>
        public string CustomerId { get; set; }

        /// <summary>
        /// The Id of the referral
        /// </summary>
        public string ReferralId { get; set; }

        /// <summary>
        /// The overall completion percentage of the purchase
        /// </summary>
        public decimal PurchaseCompletionPercentage { get; set; }

        /// <summary>
        /// The net amount price of the property
        /// </summary>
        public decimal NetPropertyAmount { get; set; }

        /// <summary>
        /// The overall completion amount of the purchase
        /// </summary>
        public decimal PurchaseCompletionAmount { get; set; }

        /// <summary>
        /// The amount so far
        /// </summary>
        public decimal InstallmentAmount { get; set; }

        /// <summary>
        /// The currency code
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Identifies whatever the payment is down-payment
        /// </summary>
        public bool IsDownPayment { get; set; }

        /// <summary>
        /// The Id of the campaign
        /// </summary>
        public Guid CampaignId { get; set; }

        /// <summary>
        /// The unique code of the location passed by salesforce
        /// </summary>
        public string UnitLocationCode { get; set; }
    }
}
