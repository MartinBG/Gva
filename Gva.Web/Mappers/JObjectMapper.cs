using AutoMapper;
using Newtonsoft.Json.Linq;

namespace Gva.Web.Mappers
{
    public class JObjectMapper : IMapper
    {
        public void CreateMap()
        {
            Mapper.CreateMap<JObject, JObject>().ConvertUsing(value =>
            {
                if (value == null)
                {
                    return null;
                }

                return new JObject(value);
            });
        }
    }
}