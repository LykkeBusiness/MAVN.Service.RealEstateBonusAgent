using System;
using System.Threading.Tasks;
using Lykke.Service.RealEstateBonusAgent.Contract.Events;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;
using Lykke.Service.Referral.Client.Enums;
using Lykke.Service.Referral.Client.Models.Requests;
using Lykke.Service.Referral.Client.Models.Responses.PropertyPurchase;
using Moq;
using Xunit;

namespace Lykke.Service.RealEstateBonusAgent.Tests.DomainServices.Services
{
    public class PropertyInstallmentPaymentServiceTest
    {
        public PropertyInstallmentPaymentServiceTest()
        {
            Fixture = new PropertyInstallmentPaymentServiceFixture();
        }

        public PropertyInstallmentPaymentServiceFixture Fixture { get; set; }

        [Fact]
        public async Task WhenNoDownPaymentIsFound_ErrorMessageShouldBeReturned()
        {
            // Arrange
            Fixture.ExistingDownPayment = null;
            Fixture.LastInstallment = null;
            var referralLeadId = Guid.NewGuid().ToString();

            // Act
            var result = await Fixture.Service.ProcessPaymentAsync(new InstallmentModel
            {
                ReferralLeadId = referralLeadId
            });

            // Assert
            Assert.False(result.IsSuccessful);
            Assert.Equal($"No down-payment for referral '{referralLeadId}' was found", result.Message);
        }

        [Fact]
        public async Task WhenDownPaymentIsFoundAndNoPreviousInstallment_FirstInstallmentAmountShouldBeUsed()
        {
            // Arrange
            var firstInstallmentAmount = 20M;
            var paidAmount = 60M;
            Fixture.ExistingDownPayment = new DownPaymentModel
            {
                FirstInstallmentAmount = firstInstallmentAmount
            };
            Fixture.LastInstallment = null;
            var referralLeadId = Guid.NewGuid().ToString();

            // Act
            var result = await Fixture.Service.ProcessPaymentAsync(new InstallmentModel
            {
                ReferralLeadId = referralLeadId,
                PaidAmount = paidAmount,
                NetPropertyPrice = 600M,
                CompletionAmount = paidAmount
            });

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(string.Empty, result.Message);

            Fixture.ReferralRealEstatePurchasePublisherMock.Verify(c => 
                c.PublishAsync(It.Is<ReferralRealEstatePurchasePaymentEvent>(x => 
                    x.PurchaseCompletionAmount == paidAmount &&
                    x.InstallmentAmount == paidAmount &&
                    x.PurchaseCompletionPercentage == 10.0M)));
        }

        [Fact]
        public async Task WhenPreviousInstallmentIsFound_CompletionAmountShouldBeUsed()
        {
            // Arrange
            var completionAmount = 20M;
            var paidAmount = 60M;
            Fixture.LastInstallment = new InstallmentModel
            {
                CompletionAmount = completionAmount
            };
            var referralLeadId = Guid.NewGuid().ToString();

            // Act
            var result = await Fixture.Service.ProcessPaymentAsync(new InstallmentModel
            {
                ReferralLeadId = referralLeadId,
                PaidAmount = paidAmount,
                NetPropertyPrice = 600M,
                CompletionAmount = paidAmount
            });

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(string.Empty, result.Message);

            Fixture.ReferralRealEstatePurchasePublisherMock.Verify(c =>
                c.PublishAsync(It.Is<ReferralRealEstatePurchasePaymentEvent>(x =>
                    x.PurchaseCompletionAmount == paidAmount &&
                    x.InstallmentAmount == paidAmount - completionAmount &&
                    x.PurchaseCompletionPercentage == 10.0M)));
        }

        [Fact]
        public async Task WhenPreviousInstallmentIsFoundAndNoReferralId_NoEventShouldBeSent()
        {
            // Arrange
            var completionAmount = 20M;
            var paidAmount = 40M;
            Fixture.LastInstallment = new InstallmentModel
            {
                CompletionAmount = completionAmount
            };

            // Act
            var result = await Fixture.Service.ProcessPaymentAsync(new InstallmentModel
            {
                PaidAmount = paidAmount,
                NetPropertyPrice = 600M,
                CompletionAmount = paidAmount
            });

            // Assert
            Assert.True(result.IsSuccessful);
            Assert.Equal(string.Empty, result.Message);

            Fixture.ReferralRealEstatePurchasePublisherMock.Verify(c =>
                c.PublishAsync(It.IsAny<ReferralRealEstatePurchasePaymentEvent>()), Times.Never);
        }
    }
}
