using AutoMapper;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Mappers
{
    public class InventoryItemMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<GvaInventoryItem, InventoryItemDO>()
                .ForMember(i => i.PartIndex, m => m.MapFrom(i => i.Part.Index.Value));
        }
    }
}
