using System;
using AutoMapper;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

namespace Gva.Api.Mappers
{
    public class RatingPartVersionMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<Tuple<PartVersion, PartVersion, PartVersion>, RatingPartVersionDO>()
                .ForMember(r => r.PartIndex, m => m.MapFrom(t => t.Item1.Part.Index))
                .ForMember(r => r.Rating, m => m.MapFrom(t => t.Item1.Content))
                .ForMember(r => r.RatingEdition, m => m.MapFrom(t => t.Item3))
                .ForMember(
                    r => r.FirstEditionValidFrom,
                    m => m.MapFrom(t => t.Item2.Content.Value<DateTime>("documentDateValidFrom")));
        }
    }
}
