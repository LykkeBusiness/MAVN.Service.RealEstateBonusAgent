using System.Threading.Tasks;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;

namespace Lykke.Service.RealEstateBonusAgent.Domain.Repositories
{
    public interface IPropertyInstallmentPaymentRepository
    {
        Task CreateAsync(InstallmentModel installment);

        Task<InstallmentModel> GetLastInstallmentAsync(string referralLeadId);
    }
}
