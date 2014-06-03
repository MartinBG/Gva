using Newtonsoft.Json.Linq;

namespace Aop.Api.WordTemplates
{
    public interface IDataGenerator
    {
        JObject Generate(JObject from);
    }
}
