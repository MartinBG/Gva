using System.ComponentModel.DataAnnotations;

namespace Gva.Api.ModelsDO.Common
{
    public class CoordinatesDO
    {
        [Range(-180, 180, ErrorMessage = "Longitude isn't between 1 and 100.")]
        public double? Longitude { get; set; }

        [Range(-180, 180, ErrorMessage = "Latitude isn't between 1 and 100.")]
        public double? Latitude { get; set; }
    }
}
