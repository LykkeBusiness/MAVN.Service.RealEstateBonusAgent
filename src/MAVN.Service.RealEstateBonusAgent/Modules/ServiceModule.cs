using Autofac;
using JetBrains.Annotations;
using Lykke.Sdk;
using MAVN.Service.RealEstateBonusAgent.Domain.Services;
using MAVN.Service.RealEstateBonusAgent.DomainServices.Services;
using MAVN.Service.RealEstateBonusAgent.MsSqlRepositories;
using MAVN.Service.RealEstateBonusAgent.Services;
using MAVN.Service.RealEstateBonusAgent.Settings;
using Lykke.Service.Referral.Client;
using Lykke.SettingsReader;

namespace MAVN.Service.RealEstateBonusAgent.Modules
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
