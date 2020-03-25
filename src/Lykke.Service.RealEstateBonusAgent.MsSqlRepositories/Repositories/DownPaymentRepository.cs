using System;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.MsSql;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;
using Lykke.Service.RealEstateBonusAgent.Domain.Repositories;
using Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Repositories
{
    public class DownPaymentRepository: IDownPaymentRepository
    {
        private readonly IDbContextFactory<RealEstateBonusAgentContext> _msSqlContextFactory;
        private readonly IMapper _mapper;

        public DownPaymentRepository(
            IDbContextFactory<RealEstateBonusAgentContext> msSqlContextFactory, 
            IMapper mapper)
        {
            _msSqlContextFactory = msSqlContextFactory;
            _mapper = mapper;
        }

        public async Task CreateOrUpdateAsync(DownPaymentModel downPayment)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var existingReferral =
                    await context.DownPayments.FirstOrDefaultAsync(e => e.ReferralLeadId == downPayment.ReferralLeadId);

                if (existingReferral != null)
                {
                    context.DownPayments.Update(_mapper.Map<DownPaymentEntity>(downPayment));
                    await context.SaveChangesAsync();

                    return;
                }

                await context.DownPayments.AddAsync(_mapper.Map<DownPaymentEntity>(downPayment));

                await context.SaveChangesAsync();
            }
        }

        public async Task<DownPaymentModel> GetByReferralLeadIdAsync(string referralLeadId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var downPayment =
                    await context.DownPayments.FirstOrDefaultAsync(c => c.ReferralLeadId == referralLeadId);

                return _mapper.Map<DownPaymentModel>(downPayment);
            }
        }
    }
}
