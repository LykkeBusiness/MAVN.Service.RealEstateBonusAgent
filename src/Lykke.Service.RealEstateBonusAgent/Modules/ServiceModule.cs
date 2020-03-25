using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using Lykke.Service.RealEstateBonusAgent.Domain.Services;
using Lykke.Service.RealEstateBonusAgent.DomainServices.Services;
using Lykke.Service.RealEstateBonusAgent.MsSqlRepositories;
using Lykke.Service.RealEstateBonusAgent.Services;
using Lykke.Service.RealEstateBonusAgent.Settings;
using Lykke.Service.Referral.Client;
using Lykke.SettingsReader;

namespace Lykke.Service.RealEstateBonusAgent.Modules
{
    [UsedImplicitly]
    public class ServiceModule : Module
    {
        private readonly IReloadingManager<AppSettings> _appSettings;

        public ServiceModule(IReloadingManager<AppSettings> appSettings)
        {
            _appSettings = appSettings;
        }

        protected override void Load(ContainerBuilder builder)
        {
            // Modules
            builder.RegisterModule(new AutofacModule(_appSettings.CurrentValue.RealEstateBonusAgentService.Db
                .MsSqlConnectionString));

            // Clients
            builder.RegisterReferralClient(_appSettings.CurrentValue.ReferralServiceClient, null);

            // Services
            builder.RegisterType<StartupManager>()
                .As<IStartupManager>()
                .SingleInstance();

            builder.RegisterType<ShutdownManager>()
                .As<IShutdownManager>()
                .AutoActivate()
                .SingleInstance();

            builder.RegisterType<DownPaymentService>()
                .As<IDownPaymentService>()
                .SingleInstance();

            builder.RegisterType<PropertyInstallmentPaymentService>()
                .As<IPropertyInstallmentPaymentService>()
                .SingleInstance();
        }
    }
}
