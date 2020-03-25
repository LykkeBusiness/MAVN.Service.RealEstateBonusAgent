using System.Threading.Tasks;
using Lykke.Logs;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.Service.RealEstateBonusAgent.Contract.Events;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;
using Lykke.Service.RealEstateBonusAgent.Domain.Repositories;
using Lykke.Service.RealEstateBonusAgent.DomainServices.Services;
using Lykke.Service.Referral.Client;
using Lykke.Service.Referral.Client.Enums;
using Lykke.Service.Referral.Client.Models.Requests;
using Lykke.Service.Referral.Client.Models.Responses.PropertyPurchase;
using Moq;

namespace Lykke.Service.RealEstateBonusAgent.Tests.DomainServices.Services
{
    public class PropertyInstallmentPaymentServiceFixture
    {
        public PropertyInstallmentPaymentServiceFixture()
        {
            Service = new PropertyInstallmentPaymentService(
                PropertyInstallmentPaymentRepositoryMock.Object,
                DownPaymentRepositoryMock.Object,
                ReferralClientMock.Object,
                ReferralRealEstatePurchasePublisherMock.Object,
                EmptyLogFactory.Instance);

            SetupMocks();
        }

        public Mock<IDownPaymentRepository> DownPaymentRepositoryMock { get; set; } = new Mock<IDownPaymentRepository>();
        public Mock<IPropertyInstallmentPaymentRepository> PropertyInstallmentPaymentRepositoryMock { get; set; }
            = new Mock<IPropertyInstallmentPaymentRepository>();
        public Mock<IReferralClient> ReferralClientMock { get; set; } = new Mock<IReferralClient>();
        public Mock<IRabbitPublisher<ReferralRealEstatePurchasePaymentEvent>> ReferralRealEstatePurchasePublisherMock { get; set; }
            = new Mock<IRabbitPublisher<ReferralRealEstatePurchasePaymentEvent>>();

        public DownPaymentModel ExistingDownPayment { get; set; } = null;

        public PropertyInstallmentPaymentService Service { get; set; }

        public RealEstatePurchaseResponse ReferralRealEstateResult { get; set; }
            = new RealEstatePurchaseResponse { ErrorCode = RealEstatePurchaseErrorCode.None };

        public InstallmentModel LastInstallment { get; set; }
            = new InstallmentModel();

        private void SetupMocks()
        {
            PropertyInstallmentPaymentRepositoryMock.Setup(c => c.GetLastInstallmentAsync(It.IsAny<string>()))
                .ReturnsAsync(() => LastInstallment);

            DownPaymentRepositoryMock.Setup(c => c.GetByReferralLeadIdAsync(It.IsAny<string>()))
                .ReturnsAsync(() => ExistingDownPayment);

            ReferralClientMock
                .Setup(c => c.ReferralLeadApi.AddRealEstatePurchase(It.IsAny<RealEstatePurchaseRequest>()))
                .ReturnsAsync(() => ReferralRealEstateResult);

            ReferralRealEstatePurchasePublisherMock
                .Setup(c => c.PublishAsync(It.IsAny<ReferralRealEstatePurchasePaymentEvent>()))
                .Returns(Task.CompletedTask);
        }
    }
}
