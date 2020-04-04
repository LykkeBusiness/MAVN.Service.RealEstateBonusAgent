using AutoMapper;
using MAVN.Service.RealEstateBonusAgent.Domain.Model;
using MAVN.Service.RealEstateBonusAgent.MsSqlRepositories.Entities;

namespace MAVN.Service.RealEstateBonusAgent.MsSqlRepositories
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
