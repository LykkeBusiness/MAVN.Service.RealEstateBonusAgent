using Autofac;
using Lykke.Common.MsSql;
using MAVN.Service.RealEstateBonusAgent.Domain.Repositories;
using MAVN.Service.RealEstateBonusAgent.MsSqlRepositories.Repositories;

namespace MAVN.Service.RealEstateBonusAgent.MsSqlRepositories
{
    public class AutofacModule : Module
    {
        private readonly string _connectionString;

        public AutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMsSql(
                _connectionString,
                connString => new RealEstateBonusAgentContext(connString, false),
                dbConn => new RealEstateBonusAgentContext(dbConn));

            builder.RegisterType<DownPaymentRepository>()
                .As<IDownPaymentRepository>()
                .SingleInstance();

            builder.RegisterType<PropertyInstallmentPaymentRepository>()
                .As<IPropertyInstallmentPaymentRepository>()
                .SingleInstance();
        }
    }
}
