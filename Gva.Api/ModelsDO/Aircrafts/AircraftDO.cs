using Regs.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDO
    {
        public AircraftDO()
        {
            AircraftData = new AircraftDataDO();
        }

        public AircraftDO(PartVersion<AircraftDataDO> aircraftDataPart)
        {
            this.AircraftData = aircraftDataPart.Content;
        }

        public AircraftDataDO AircraftData { get; set; }
    }
}
