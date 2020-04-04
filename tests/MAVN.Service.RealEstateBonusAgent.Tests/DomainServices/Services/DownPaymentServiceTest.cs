using System;
using System.Threading.Tasks;
using MAVN.Service.RealEstateBonusAgent.Contract.Events;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;
using Lykke.Service.Referral.Client.Enums;
using Lykke.Service.Referral.Client.Models.Requests;
using Lykke.Service.Referral.Client.Models.Responses.PropertyPurchase;
using Moq;
using Xunit;

namespace MAVN.Service.RealEstateBonusAgent.Tests.DomainServices.Services
{
    public class DownPaymentServiceTests
    {
        public DownPaymentServiceTests()
        {
            Fixture = new DownPaymentServiceFixture();
        }

        public DownPaymentServiceFixture Fixture { get; set; }

        [Fact]
        public async Task WhenExistingDownPaymentExists_ErrorMessageShouldBeReturned()
        {
            // Arrange
            Fixture.ExistingDownPayment = new DownPaymentModel();
            var referralLeadId = Guid.NewGuid().ToString();

            // Act
            var result = await Fixture.Service.ProcessDownPaymentAsync(new DownPaymentModel
            {
                ReferralLeadId = referralLeadId
            });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal($"Down-payment for referral '{referralLeadId}' already exists.", result.Message);
        }

        [Fact]
        public async Task WhenReferralCreationFails_ErrorMessageShouldBeReturned()
        {
            // Arrange
            Fixture.ReferralRealEstateResult = new RealEstatePurchaseResponse
            {
                ErrorCode = RealEstatePurchaseErrorCode.ReferralNotFound
            };
            var referralLeadId = Guid.NewGuid().ToString();

            // Act
            var result = await Fixture.Service.ProcessDownPaymentAsync(new DownPaymentModel
            {
                ReferralLeadId = referralLeadId
            });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal($"The Referral Real estate creation failed with error code '{RealEstatePurchaseErrorCode.ReferralNotFound}'", result.Message);
        }

        [Fact]
        public async Task WhenReferralLeadIsNotReceived_PublishAndReferralCreationShouldBeSkipped()
        {
            // Arrange
            // Act
            var result = await Fixture.Service.ProcessDownPaymentAsync(new DownPaymentModel
            {
                ReferralLeadId = null
            });

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(string.Empty, result.Message);

            Fixture.ReferralClientMock.Verify(c => c.ReferralLeadApi.AddRealEstatePurchase(It.IsAny<RealEstatePurchaseRequest>()), Times.Never);
            Fixture.ReferralRealEstatePurchasePublisherMock.Verify(c => c.PublishAsync(It.IsAny<ReferralRealEstatePurchasePaymentEvent>()), Times.Never);
        }

        [Fact]
        public async Task WhenReferralLeadIsReceived_ShouldCreateDownPaymentAndPublishEvent()
        {
            // Arrange
            var referralLeadId = Guid.NewGuid().ToString();

            // Act
            var result = await Fixture.Service.ProcessDownPaymentAsync(new DownPaymentModel
            {
                ReferralLeadId = referralLeadId
            });

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(string.Empty, result.Message);

            Fixture.ReferralClientMock.Verify(c => c.ReferralLeadApi.AddRealEstatePurchase(It.IsAny<RealEstatePurchaseRequest>()), Times.Once);
            Fixture.ReferralRealEstatePurchasePublisherMock.Verify(c => c.PublishAsync(It.IsAny<ReferralRealEstatePurchasePaymentEvent>()), Times.Never);
        }
    }
}
