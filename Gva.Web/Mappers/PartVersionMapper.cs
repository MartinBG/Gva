using AutoMapper;
using Gva.Web.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Web.Mappers
{
    public class PartVersionMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<PartVersion, PartVersionDO>()
                .ForMember(pdo => pdo.PartIndex, m => m.MapFrom(p => p.Part.Index))
                .ForMember(pdo => pdo.Part, m => m.MapFrom(p => JObject.Parse( p.TextContent)));
        }
    }
}