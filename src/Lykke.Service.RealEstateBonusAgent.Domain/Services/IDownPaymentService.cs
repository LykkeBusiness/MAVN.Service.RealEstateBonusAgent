using System.Threading.Tasks;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;

namespace Lykke.Service.RealEstateBonusAgent.Domain.Services
{
    public interface IDownPaymentService
    {
        Task<(bool IsSuccessful, string Message)> ProcessDownPaymentAsync(DownPaymentModel downPayment);
    }
}
