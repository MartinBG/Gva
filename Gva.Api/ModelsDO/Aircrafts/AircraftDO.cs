using Regs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDO
    {
        public AircraftDO()
        {
            AircraftData = new AircraftDataDO();
        }

        public AircraftDO(PartVersion aircraftDataPart)
        {
            this.AircraftData = aircraftDataPart.Content.ToObject<AircraftDataDO>();
        }

        public AircraftDataDO AircraftData { get; set; }
    }
}
