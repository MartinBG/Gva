using AutoMapper;
using Gva.Api.ModelsDO;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Mappers
{
    public class PartVersionMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<PartVersion, PartVersionDO>().ConvertUsing(pv =>
            {
                if (pv == null)
                {
                    return null;
                }

                return new PartVersionDO()
                {
                    PartIndex = pv.Part.Index.Value,
                    Part = pv.Content
                };
            });
        }
    }
}