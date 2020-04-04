using System.Threading.Tasks;
using Lykke.Common.Log;
using Lykke.Service.MAVNPropertyIntegration.Contract.MAVNEvents;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;
using MAVN.Service.RealEstateBonusAgent.Domain.Services;

namespace MAVN.Service.RealEstateBonusAgent.DomainServices.Subscribers
{
    public class RealEstatePropertyInstallmentPaymentSubscriber : RabbitSubscriber<MAVNPropertyInstallmentPaymentEvent>
    {
        private readonly IPropertyInstallmentPaymentService _propertyInstallmentPaymentService;

        public RealEstatePropertyInstallmentPaymentSubscriber(
            string connectionString,
            string exchangeName,
            IPropertyInstallmentPaymentService propertyInstallmentPaymentService,
            ILogFactory logFactory)
            : base(connectionString, exchangeName, logFactory)
        {
            _propertyInstallmentPaymentService = propertyInstallmentPaymentService;
        }

        protected override async Task<(bool isSuccessful, string errorMessage)> ProcessMessageAsync(
            MAVNPropertyInstallmentPaymentEvent message)
        {
            if (message.NetPropertyPrice == null)
            {
                Log.Warning("Invalid NetProperty price in the Installment event.", context: message);
            }

            return await _propertyInstallmentPaymentService.ProcessPaymentAsync(new InstallmentModel
            {
                InstallmentNumber = message.InstallmentNumber,
                TotalInstallments = message.TotalInstallments,
                Timestamp = message.Timestamp,
                PaidAmount = 0, // Calculation logic changed. Now we use CompletionAmount for calculation on our side.
                CurrencyCode = "AED", // Supposedly coming as AED in the event
                NetPropertyPrice = message.NetPropertyPrice ?? 0,
                ReferralLeadId = message.MVNReferralId,
                ReferrerId = message.AgentCustomerId,
                CustomerId = message.BuyerCustomerId,
                UnitLocationCode = message.UnitLocationCode,
                CompletionAmount = message.TotalAmountPaid
            });
        }
    }
}
