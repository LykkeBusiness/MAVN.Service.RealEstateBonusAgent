using System.Threading.Tasks;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;

namespace MAVN.Service.RealEstateBonusAgent.Domain.Repositories
{
    public interface IPropertyInstallmentPaymentRepository
    {
        Task CreateAsync(InstallmentModel installment);

        Task<InstallmentModel> GetLastInstallmentAsync(string referralLeadId);
    }
}
