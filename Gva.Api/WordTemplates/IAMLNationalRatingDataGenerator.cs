using Newtonsoft.Json.Linq;

namespace Gva.Api.WordTemplates
{
    public interface IAMLNationalRatingDataGenerator
    {
        object GetData(int lotId, string path, int ratingPartIndex, int editionPartIndex);

        string GeneratorCode { get; }

        string GeneratorName { get; }
    }
}
