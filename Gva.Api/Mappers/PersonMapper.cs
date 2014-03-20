using AutoMapper;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Mappers
{
    public class PersonMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<GvaPerson, PersonDO>()
                .ForMember(p => p.Id, m => m.MapFrom(p => p.GvaPersonLotId));
        }
    }
}