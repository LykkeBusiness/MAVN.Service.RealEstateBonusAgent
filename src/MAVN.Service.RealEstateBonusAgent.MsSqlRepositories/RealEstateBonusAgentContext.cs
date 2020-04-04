using System.Data.Common;
using JetBrains.Annotations;
using Lykke.Common.MsSql;
using MAVN.Service.RealEstateBonusAgent.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.RealEstateBonusAgent.MsSqlRepositories
{
    public class RealEstateBonusAgentContext: MsSqlContext
    {
        private const string Schema = "realestatebonusagent";

        public DbSet<DownPaymentEntity> DownPayments { get; set; }

        public DbSet<InstallmentEntity> Installments { get; set; }

        // C-tor for EF migrations
        [UsedImplicitly]
        public RealEstateBonusAgentContext()
            : base(Schema)
        {
        }

        public RealEstateBonusAgentContext(DbContextOptions contextOptions)
            : base(Schema, contextOptions)
        {
        }

        public RealEstateBonusAgentContext(string schema, DbContextOptions contextOptions)
            : base(schema, contextOptions)
        {
        }

        public RealEstateBonusAgentContext(string connectionString, bool isTraceEnabled)
            : base(Schema, connectionString, isTraceEnabled)
        {
        }

        public RealEstateBonusAgentContext(DbConnection dbConnection)
            : base(Schema, dbConnection)
        {
        }

        protected override void OnLykkeModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
