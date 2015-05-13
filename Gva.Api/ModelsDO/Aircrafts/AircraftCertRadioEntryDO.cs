using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertRadioEntryDO
    {
        public NomValue Equipment { get; set; }

        public string Count { get; set; }

        public string OtherType { get; set; }

        public string Power { get; set; }

        public string Class { get; set; }

        public string Bandwidth { get; set; }
    }
}
