using AutoMapper;
using Common.Api.Models;
using Gva.Api.Models;

namespace Gva.Api.Mappers
{
    public class CaseTypeNomMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<GvaCaseType, NomValue>()
                .ForMember(nv => nv.NomValueId, m => m.MapFrom(ct => ct.GvaCaseTypeId));
        }
    }
}
