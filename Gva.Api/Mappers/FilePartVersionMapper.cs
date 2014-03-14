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
            Mapper.CreateMap<PartVersion, FilePartVersionDO>()
                .ForMember(pv => pv.PartIndex, m => m.MapFrom(p => p.Part.Index))
                .ForMember(pv => pv.Part, m => m.MapFrom(p => p.Content))
                .ForMember(pv => pv.Files, m => m.ResolveUsing<FileResolver>().FromMember(p => Tuple.Create(p.Part.LotId, p.PartId)));
        }
    }
}
