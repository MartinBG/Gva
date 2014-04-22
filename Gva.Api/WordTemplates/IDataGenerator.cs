using Newtonsoft.Json.Linq;

namespace Gva.Api.WordTemplates
{
    public interface IDataGenerator
    {
        JObject GetData(int lotId, string path, int index);

        string[] TemplateNames { get; }
    }
}
