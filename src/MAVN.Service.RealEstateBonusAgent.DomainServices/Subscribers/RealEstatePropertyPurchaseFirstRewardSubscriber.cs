using System.Threading.Tasks;
using Lykke.Common.Log;
using Lykke.Service.MAVNPropertyIntegration.Contract.MAVNEvents;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;
using MAVN.Service.RealEstateBonusAgent.Domain.Services;

namespace MAVN.Service.RealEstateBonusAgent.DomainServices.Subscribers
{
    public class RealEstatePropertyPurchaseFirstRewardSubscriber : RabbitSubscriber<MAVNPropertyPurchaseFirstRewardEvent>
    {
        private readonly IDownPaymentService _downPaymentService;

        public RealEstatePropertyPurchaseFirstRewardSubscriber(
            string connectionString,
            string exchangeName,
            IDownPaymentService downPaymentService,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, logFactory)
        {
            _downPaymentService = downPaymentService;
        }

        protected override async Task<(bool isSuccessful, string errorMessage)> ProcessMessageAsync(
            MAVNPropertyPurchaseFirstRewardEvent message)
        {
            if (message.NetPropertyPrice == null)
            {
                Log.Warning("Invalid NetProperty price in the Installment event.", context: message);
            }

            return await _downPaymentService.ProcessDownPaymentAsync(new DownPaymentModel
            {
                ReferralLeadId = message.MVNReferralId,
                Timestamp = message.Timestamp,
                NetPropertyPrice = message.NetPropertyPrice ?? 0,
                CalculatedCommissionAmount = message.CalculatedCommissionAmount,
                CurrencyCode = "AED", // Supposedly coming as AED in the event
                IsFullReward = message.IsFullReward,
                DownPaymentPercentage = message.DownpaymentPercentage,
                FirstInstallmentAmount = message.FirstInstallmentAmount,
                ReferrerId = message.AgentCustomerId,
                CustomerId = message.BuyerCustomerId,
                UnitLocationCode = message.UnitLocationCode
            });
        }
    }
}
