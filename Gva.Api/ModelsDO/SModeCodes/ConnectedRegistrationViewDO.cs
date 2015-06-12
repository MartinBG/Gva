using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;
using Gva.Api.Models.Views.SModeCode;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO.SModeCodes
{
    public class ConnectedRegistrationViewDO
    {
        public ConnectedRegistrationViewDO(PartVersion<AircraftCertRegistrationFMDO> registration)
        {
            this.LotId = registration.Part.LotId;
            this.PartIndex = registration.Part.Index;
            this.ActNumber = registration.Content.ActNumber;
            this.CertNumber = registration.Content.CertNumber;
            this.CertDate = registration.Content.CertDate;
            this.IsActive = registration.Content.IsActive;
            this.IsCurrent = registration.Content.IsCurrent;
            this.RegisterCode = registration.Content.Register.Code;
        }

        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int? ActNumber { get; set; }

        public int? CertNumber { get; set; }

        public DateTime? CertDate { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsCurrent { get; set; }

        public string RegisterCode { get; set; }
    }
}
