using Autofac;
using JetBrains.Annotations;
using Lykke.Common;
using Lykke.RabbitMqBroker.Publisher;
using Lykke.Service.RealEstateBonusAgent.Contract.Events;
using Lykke.Service.RealEstateBonusAgent.DomainServices.Subscribers;
using Lykke.Service.RealEstateBonusAgent.Settings;
using Lykke.SettingsReader;

namespace Lykke.Service.RealEstateBonusAgent.Modules
{
    [UsedImplicitly]
    public class RabbitMqModule: Module
    {
        // Subscribers
        private const string RealEstatePropertyPurchasedFirstExchangeName = "lykke.realestateintegration.propertypurchasefirstreward";
        private const string RealEstatePropertyInstallmentPaymentExchangeName = "lykke.realestateintegration.propertyinstallmentpayment";

        //Publishers
        private const string ReferralDownPaymentExchangeName = "lykke.bonus.realestatepurchasepayment";

        private readonly RabbitMqSettings _settings;

        public RabbitMqModule(IReloadingManager<AppSettings> appSettings)
        {
            _settings = appSettings.CurrentValue.RealEstateBonusAgentService.Rabbit;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Subscribers
            builder.RegisterType<RealEstatePropertyInstallmentPaymentSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", _settings.ConnectionString)
                .WithParameter("exchangeName", RealEstatePropertyInstallmentPaymentExchangeName);

            builder.RegisterType<RealEstatePropertyPurchaseFirstRewardSubscriber>()
                .As<IStartStop>()
                .SingleInstance()
                .WithParameter("connectionString", _settings.ConnectionString)
                .WithParameter("exchangeName", RealEstatePropertyPurchasedFirstExchangeName);

            // Publishers
            builder.RegisterJsonRabbitPublisher<ReferralRealEstatePurchasePaymentEvent>(
                _settings.ConnectionString,
                ReferralDownPaymentExchangeName);
        }
    }
}
