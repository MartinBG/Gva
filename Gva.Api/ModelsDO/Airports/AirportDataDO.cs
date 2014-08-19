using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Airports
{
    public class AirportDataDO
    {
        public AirportDataDO()
        {
            this.Coordinates = new CoordinatesDO();
            this.Frequencies = new List<AirportFrequencyDO>();
            this.RadioNavigationAids = new List<AirportRadioNavAid>();
        }

        [Required(ErrorMessage = "AirportType is required.")]
        public NomValue AirportType { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "NameAlt is required.")]
        public string NameAlt { get; set; }

        public string Icao { get; set; }

        public string Place { get; set; }

        public CoordinatesDO Coordinates { get; set; }

        public string Runway { get; set; }

        public string Course { get; set; }

        public string Excess { get; set; }

        public string Concrete { get; set; }

        public List<AirportFrequencyDO> Frequencies { get; set; }

        public List<AirportRadioNavAid> RadioNavigationAids { get; set; }
    }
}
