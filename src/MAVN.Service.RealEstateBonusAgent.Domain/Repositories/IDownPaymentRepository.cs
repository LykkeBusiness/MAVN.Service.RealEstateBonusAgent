using System.Threading.Tasks;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;

namespace MAVN.Service.RealEstateBonusAgent.Domain.Repositories
{
    public interface IDownPaymentRepository
    {
        Task CreateOrUpdateAsync(DownPaymentModel downPayment);
        Task<DownPaymentModel> GetByReferralLeadIdAsync(string referralLeadId);
    }
}
