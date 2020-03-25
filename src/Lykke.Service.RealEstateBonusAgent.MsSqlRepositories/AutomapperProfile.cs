using AutoMapper;
using Lykke.Service.RealEstateBonusAgent.Domain.Model;
using Lykke.Service.RealEstateBonusAgent.MsSqlRepositories.Entities;

namespace Lykke.Service.RealEstateBonusAgent.MsSqlRepositories
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<DownPaymentModel, DownPaymentEntity>(MemberList.Destination);
            CreateMap<DownPaymentEntity, DownPaymentModel>(MemberList.Destination);

            CreateMap<InstallmentModel, InstallmentEntity>(MemberList.Destination);
            CreateMap<InstallmentEntity, InstallmentModel>(MemberList.Destination);
        }
    }
}
