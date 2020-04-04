using System.Threading.Tasks;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;

namespace MAVN.Service.RealEstateBonusAgent.Domain.Services
{
    public interface IDownPaymentService
    {
        Task<(bool IsSuccessful, string Message)> ProcessDownPaymentAsync(DownPaymentModel downPayment);
    }
}
