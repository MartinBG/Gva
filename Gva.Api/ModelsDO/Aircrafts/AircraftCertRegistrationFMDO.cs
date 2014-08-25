using System;
using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertRegistrationFMDO
    {
        public AircraftCertRegistrationFMDO()
        {
            this.CatAW = new List<NomValue>();
            this.Limitations = new List<NomValue>();
        }

        public int? CertNumber { get; set; }

        public NomValue Register { get; set; }

        public int? ActNumber { get; set; }

        public string RegMark { get; set; }

        public DateTime? CertDate { get; set; }

        public string IncomingDocNumber { get; set; }

        public DateTime? IncomingDocDate { get; set; }

        public string IncomingDocDesc { get; set; }

        public NomValue OwnerOrganization { get; set; }

        public NomValue OwnerPerson { get; set; }

        public NomValue OperOrganization { get; set; }

        public NomValue OperPerson { get; set; }

        public List<NomValue> CatAW { get; set; }

        public List<NomValue> Limitations { get; set; }

        public NomValue OperationType { get; set; }

        public string LeasingDocNumber { get; set; }

        public DateTime? LeasingDocDate { get; set; }

        public DateTime? LeasingEndDate { get; set; }

        public NomValue LesorOrganization { get; set; }

        public NomValue LessorPerson { get; set; }

        public string LeasingAgreement { get; set; }

        public AircraftCertDeregFMDO Removal { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsCurrent { get; set; }

        public bool? OwnerIsOrg { get; set; }

        public bool? OperIsOrg { get; set; }

        public bool? LessorIsOrg { get; set; }
    }
}
