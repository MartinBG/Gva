using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Common.Json;
using Gva.Api.Models.Views.Aircraft;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftRegistrationDO
    {
        public AircraftRegistrationDO(GvaViewAircraftRegistration registration)
        {
            this.LotId = registration.LotId;
            this.PartIndex = registration.PartIndex;
            this.CertRegisterId = registration.CertRegisterId;
            this.CertNumber = registration.CertNumber;
            this.ActNumber = registration.ActNumber;
            this.RegisterCode = registration.Register.Code;
            this.RegMark = registration.RegMark;
        }

        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int? CertRegisterId { get; set; }

        public int? CertNumber { get; set; }

        public int? ActNumber { get; set; }

        public string RegMark { get; set; }

        public string RegisterCode { get; set; }
    }
}
