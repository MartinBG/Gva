using AutoMapper;
using Gva.Api.Mappers.Resolvers;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Mappers
{
    public class InventoryItemMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<GvaInventoryItem, InventoryItemDO>()
                .ForMember(i => i.PartIndex, m => m.MapFrom(i => i.Part.Index.Value))
                .ForMember(i => i.Files, m => m.ResolveUsing<FileResolver>().FromMember(i => i.PartId));
        }
    }
}
