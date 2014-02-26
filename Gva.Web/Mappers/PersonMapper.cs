using AutoMapper;
using Gva.Api.Models;
using Gva.Web.Models;

namespace Gva.Web.Mappers
{
    public class PersonMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<GvaPerson, Person>()
                .ForMember(p => p.Id, m => m.MapFrom(p => p.GvaPersonLotId));
        }
    }
}