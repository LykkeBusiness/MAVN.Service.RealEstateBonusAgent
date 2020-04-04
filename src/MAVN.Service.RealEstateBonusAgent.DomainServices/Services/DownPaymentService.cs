using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Service.RealEstateBonusAgent.Contract.Events;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;
using MAVN.Service.RealEstateBonusAgent.Domain.Repositories;
using MAVN.Service.RealEstateBonusAgent.Domain.Services;
using Lykke.Service.Referral.Client;
using Lykke.Service.Referral.Client.Enums;
using Lykke.Service.Referral.Client.Models.Requests;

namespace MAVN.Service.RealEstateBonusAgent.DomainServices.Services
{
    public class DownPaymentService: IDownPaymentService
    {
        private readonly IDownPaymentRepository _downPaymentRepository;
        private readonly IReferralClient _referralClient;
        private readonly IRabbitPublisher<ReferralRealEstatePurchasePaymentEvent> _referralDownPaymentEventPublisher;
        private readonly ILog _log;

        public DownPaymentService(
            IDownPaymentRepository downPaymentRepository,
            IReferralClient referralClient,
            IRabbitPublisher<ReferralRealEstatePurchasePaymentEvent> referralDownPaymentEventPublisher,
            ILogFactory logFactory)
        {
            _downPaymentRepository = downPaymentRepository;
            _referralClient = referralClient;
            _referralDownPaymentEventPublisher = referralDownPaymentEventPublisher;
            _log = logFactory.CreateLog(this);
        }

        public async Task<(bool IsSuccessful, string Message)> ProcessDownPaymentAsync(DownPaymentModel downPayment)
        {
            var existingDownPayment = await _downPaymentRepository.GetByReferralLeadIdAsync(downPayment.ReferralLeadId);
            if (existingDownPayment != null)
            {
                return (false, $"Down-payment for referral '{downPayment.ReferralLeadId}' already exists.");
            }
            
            if (!string.IsNullOrEmpty(downPayment.ReferralLeadId))
            {
                var result = await _referralClient.ReferralLeadApi.AddRealEstatePurchase(new RealEstatePurchaseRequest
                {
                    ReferralId = downPayment.ReferralLeadId,
                    Timestamp = downPayment.Timestamp
                });

                if (result.ErrorCode != RealEstatePurchaseErrorCode.None)
                {
                    var message = $"The Referral Real estate creation failed with error code '{result.ErrorCode}'";
                    _log.Error(message: message, context: downPayment);

                    return (false, message);
                }

                //// Commented to avoid double bonuses. Down-payment should not trigger bonus payment.
                //// Down-payment amount will be received in the first installment.
                //
                // await _referralDownPaymentEventPublisher.PublishAsync(new ReferralRealEstatePurchasePaymentEvent
                // {
                //     CustomerId = downPayment.ReferrerId,
                //     ReferralId = downPayment.ReferralLeadId,
                //     InstallmentAmount = downPayment.FirstInstallmentAmount,
                //     CurrencyCode = downPayment.CurrencyCode,
                //     NetPropertyAmount = downPayment.NetPropertyPrice,
                //     PurchaseCompletionAmount = downPayment.FirstInstallmentAmount,
                //     PurchaseCompletionPercentage = downPayment.DownPaymentPercentage,
                //     IsDownPayment = true,
                //     CampaignId = result.CampaignId,
                //     UnitLocationCode = downPayment.UnitLocationCode
                // });
            }

            await _downPaymentRepository.CreateOrUpdateAsync(downPayment);

            return (true, string.Empty);
        }
    }
}
