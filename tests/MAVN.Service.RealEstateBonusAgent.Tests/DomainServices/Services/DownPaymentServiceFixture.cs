using System.Threading.Tasks;
using Lykke.Logs;
using Lykke.RabbitMqBroker.Publisher;
using MAVN.Service.RealEstateBonusAgent.Contract.Events;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;
using MAVN.Service.RealEstateBonusAgent.Domain.Repositories;
using MAVN.Service.RealEstateBonusAgent.DomainServices.Services;
using Lykke.Service.Referral.Client;
using Lykke.Service.Referral.Client.Enums;
using Lykke.Service.Referral.Client.Models.Requests;
using Lykke.Service.Referral.Client.Models.Responses.PropertyPurchase;
using Moq;

namespace MAVN.Service.RealEstateBonusAgent.Tests.DomainServices.Services
{
    public class DownPaymentServiceFixture
    {
        public DownPaymentServiceFixture()
        {
            Service = new DownPaymentService(
                DownPaymentRepositoryMock.Object,
                ReferralClientMock.Object,
                ReferralRealEstatePurchasePublisherMock.Object,
                EmptyLogFactory.Instance);

            SetupMocks();
        }

        public Mock<IDownPaymentRepository> DownPaymentRepositoryMock { get; set; } = new Mock<IDownPaymentRepository>();
        public Mock<IReferralClient> ReferralClientMock { get; set; } = new Mock<IReferralClient>();
        public Mock<IRabbitPublisher<ReferralRealEstatePurchasePaymentEvent>> ReferralRealEstatePurchasePublisherMock { get; set; }
            = new Mock<IRabbitPublisher<ReferralRealEstatePurchasePaymentEvent>>();

        public DownPaymentModel ExistingDownPayment { get; set; } = null;

        public DownPaymentService Service { get; set; }

        public RealEstatePurchaseResponse ReferralRealEstateResult { get; set; }
            = new RealEstatePurchaseResponse { ErrorCode = RealEstatePurchaseErrorCode.None };

        private void SetupMocks()
        {
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
