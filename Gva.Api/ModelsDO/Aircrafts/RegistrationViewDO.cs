
namespace Gva.Api.ModelsDO.Aircrafts
{
    public class RegistrationViewDO
    {
        public int FirstIndex { get; set; }

        public int? PrevIndex { get; set; }

        public int? CurrentIndex { get; set; }

        public int? NextIndex { get; set; }

        public int LastIndex { get; set; }

        public int? AirworthinessIndex { get; set; }

        public bool HasAirworthiness { get; set; }

        public AircraftCertRegistrationFMDO FirstReg { get; set; }

        public AircraftCertRegistrationFMDO LastReg { get; set; }
    }
}
