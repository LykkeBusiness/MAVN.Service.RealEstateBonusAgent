using System.Threading.Tasks;
using Common.Log;
using Lykke.Common.Log;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.Service.RealEstateBonusAgent.Contract.Events;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;
using Lykke.Service.RealEstateBonusAgent.Domain.Repositories;
using Lykke.Service.RealEstateBonusAgent.Domain.Services;
using Lykke.Service.Referral.Client;
using Lykke.Service.Referral.Client.Enums;
using Lykke.Service.Referral.Client.Models.Requests;

namespace Lykke.Service.RealEstateBonusAgent.DomainServices.Services
{
    public class PropertyInstallmentPaymentService: IPropertyInstallmentPaymentService
    {
        private readonly IPropertyInstallmentPaymentRepository _propertyInstallmentPaymentRepository;
        private readonly IDownPaymentRepository _downPaymentRepository;
        private readonly IReferralClient _referralClient;
        private readonly IRabbitPublisher<ReferralRealEstatePurchasePaymentEvent> _referralPaymentEventPublisher;
        private readonly ILog _log;

        public PropertyInstallmentPaymentService(
            IPropertyInstallmentPaymentRepository propertyInstallmentPaymentRepository,
            IDownPaymentRepository downPaymentRepository,
            IReferralClient referralClient,
            IRabbitPublisher<ReferralRealEstatePurchasePaymentEvent> referralPaymentEventPublisher,
            ILogFactory logFactory)
        {
            _propertyInstallmentPaymentRepository = propertyInstallmentPaymentRepository;
            _downPaymentRepository = downPaymentRepository;
            _referralClient = referralClient;
            _referralPaymentEventPublisher = referralPaymentEventPublisher;
            _log = logFactory.CreateLog(this);
        }

        public async Task<(bool IsSuccessful, string Message)> ProcessPaymentAsync(InstallmentModel installment)
        {
            decimal previousCompletionAmount;
            decimal netPrice;

            var previousInstallment = await _propertyInstallmentPaymentRepository
                .GetLastInstallmentAsync(installment.ReferralLeadId);

            // Then purchase already exists this will return the campaign id without creating a new one
            var referralResult = await _referralClient.ReferralLeadApi.AddRealEstatePurchase(new RealEstatePurchaseRequest
            {
                ReferralId = installment.ReferralLeadId,
                Timestamp = installment.Timestamp
            });

            if (referralResult.ErrorCode != RealEstatePurchaseErrorCode.None)
            {
                var message = $"The Referral Real estate creation failed with error code '{referralResult.ErrorCode}'";
                _log.Error(message: message, context: installment);

                return (false, message);
            }

            if (previousInstallment == null)
            {
                var downPayment = await _downPaymentRepository.GetByReferralLeadIdAsync(installment.ReferralLeadId);

                if (downPayment == null)
                {
                    return (false, $"No down-payment for referral '{installment.ReferralLeadId}' was found");
                }

                previousCompletionAmount = 0M;
                netPrice = downPayment.NetPropertyPrice;
            }
            else
            {
                previousCompletionAmount = previousInstallment.CompletionAmount;
                netPrice = previousInstallment.NetPropertyPrice;
            }

            installment.PaidAmount = installment.CompletionAmount - previousCompletionAmount;

            if (installment.PaidAmount <= 0)
            {
                const string errorMessage = "Received payment does not increase total paid amount.";
                
                _log.Warning(errorMessage, null, new
                {
                    installment, previousInstallment
                });

                return (true, errorMessage);
            }

            await _propertyInstallmentPaymentRepository.CreateAsync(installment);

            if (!string.IsNullOrEmpty(installment.ReferralLeadId))
            {
                netPrice = installment.NetPropertyPrice != 0 
                    ? installment.NetPropertyPrice 
                    : netPrice;

                var purchaseCompletionPercentage = netPrice != 0 
                        ? (installment.CompletionAmount / netPrice) * 100M 
                        : 0;

                await _referralPaymentEventPublisher.PublishAsync(new ReferralRealEstatePurchasePaymentEvent
                {
                    CustomerId = installment.ReferrerId,
                    ReferralId = installment.ReferralLeadId,
                    InstallmentAmount = installment.PaidAmount,
                    CurrencyCode = installment.CurrencyCode,
                    NetPropertyAmount = installment.NetPropertyPrice,
                    PurchaseCompletionAmount = installment.CompletionAmount,
                    PurchaseCompletionPercentage = purchaseCompletionPercentage,
                    IsDownPayment = false,
                    CampaignId = referralResult.CampaignId,
                    UnitLocationCode = installment.UnitLocationCode
                });
            }

            return (true, string.Empty);
        }
    }
}
