using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lykke.Common.MsSql;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;
using MAVN.Service.RealEstateBonusAgent.Domain.Repositories;
using MAVN.Service.RealEstateBonusAgent.MsSqlRepositories.Entities;
using Microsoft.EntityFrameworkCore;

namespace MAVN.Service.RealEstateBonusAgent.MsSqlRepositories.Repositories
{
    public class PropertyInstallmentPaymentRepository: IPropertyInstallmentPaymentRepository
    {
        private readonly MsSqlContextFactory<RealEstateBonusAgentContext> _msSqlContextFactory;
        private readonly IMapper _mapper;

        public PropertyInstallmentPaymentRepository(
            MsSqlContextFactory<RealEstateBonusAgentContext> msSqlContextFactory, 
            IMapper mapper)
        {
            _msSqlContextFactory = msSqlContextFactory;
            _mapper = mapper;
        }

        public async Task CreateAsync(InstallmentModel installment)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                await context.Installments.AddAsync(_mapper.Map<InstallmentEntity>(installment));

                await context.SaveChangesAsync();
            }
        }

        public async Task<InstallmentModel> GetLastInstallmentAsync(string referralLeadId)
        {
            using (var context = _msSqlContextFactory.CreateDataContext())
            {
                var installment = await context.Installments
                    .Where(c => c.ReferralLeadId == referralLeadId)
                    .OrderByDescending(i => i.CompletionAmount)
                    .FirstOrDefaultAsync();

                return _mapper.Map<InstallmentModel>(installment);
            }
        }
    }
}
