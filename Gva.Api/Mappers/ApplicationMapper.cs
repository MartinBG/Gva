using AutoMapper;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Mappers
{
    public class ApplicationMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<GvaApplication, ApplicationDO>()
                .ForMember(a => a.ApplicationId, m => m.MapFrom(ga => ga.GvaApplicationId))
                .ForMember(a => a.PartIndex, m => m.MapFrom(ga => ga.GvaAppLotPart.Index))
                .ForMember(a => a.ApplicationName, m => m.ResolveUsing(ga =>
                    (ga.GvaAppLotPart.Lot.GetPart(ga.GvaAppLotPart.Path).Content as dynamic).applicationType.name));
        }
    }
}
