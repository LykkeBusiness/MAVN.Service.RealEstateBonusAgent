using System.Threading.Tasks;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;

namespace Lykke.Service.RealEstateBonusAgent.Domain.Services
{
    public interface IPropertyInstallmentPaymentService
    {
        Task<(bool IsSuccessful, string Message)> ProcessPaymentAsync(InstallmentModel installment);
    }
}
