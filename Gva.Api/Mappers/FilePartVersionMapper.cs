using System;
using AutoMapper;
using Gva.Api.Mappers.Resolvers;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

namespace Gva.Api.Mappers
{
    public class FilePartVersionMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<Tuple<PartVersion, int?, FileResolver>, FilePartVersionDO>()
                .ForMember(pv => pv.PartIndex, m => m.MapFrom(t => t.Item1.Part.Index))
                .ForMember(pv => pv.Part, m => m.MapFrom(t => t.Item1.Content))
                .ForMember(pv => pv.Files, m => m.ResolveUsing(t => t.Item3.Resolve(Tuple.Create(t.Item1.Part.LotId, t.Item1.PartId, t.Item2))));
        }
    }
}
