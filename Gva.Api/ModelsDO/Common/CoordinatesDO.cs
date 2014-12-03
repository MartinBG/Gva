using System.ComponentModel.DataAnnotations;

namespace Gva.Api.ModelsDO.Common
{
    public class CoordinatesDO
    {
        [Range(-180, 180, ErrorMessage = "Longitude isn't between -180 and 180.")]
        public double? Longitude { get; set; }

        [Range(-90, 90, ErrorMessage = "Latitude isn't between -90 and 90.")]
        public double? Latitude { get; set; }
    }
}
