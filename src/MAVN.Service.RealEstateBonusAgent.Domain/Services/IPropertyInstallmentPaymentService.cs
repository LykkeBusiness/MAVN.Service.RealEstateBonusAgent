using System.Threading.Tasks;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;

namespace MAVN.Service.RealEstateBonusAgent.Domain.Services
{
    public interface IPropertyInstallmentPaymentService
    {
        Task<(bool IsSuccessful, string Message)> ProcessPaymentAsync(InstallmentModel installment);
    }
}
