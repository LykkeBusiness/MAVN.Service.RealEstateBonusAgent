using System.Threading.Tasks;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;

namespace Lykke.Service.RealEstateBonusAgent.Domain.Repositories
{
    public interface IDownPaymentRepository
    {
        Task CreateOrUpdateAsync(DownPaymentModel downPayment);
        Task<DownPaymentModel> GetByReferralLeadIdAsync(string referralLeadId);
    }
}
