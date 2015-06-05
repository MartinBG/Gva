using Common.Api.Models;
using Newtonsoft.Json.Linq;

namespace Gva.Api.WordTemplates
{
    public interface IDataGenerator
    {
        object GetData(int lotId, string path);

        string GeneratorCode { get; }

        string GeneratorName { get; }
    }
}
