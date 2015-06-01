using System;
using Common.Api.Models;
using Gva.Api.Models.Views.Aircraft;
using Regs.Api.Models;

namespace Gva.Api.ModelsDO.SModeCodes
{
    public class SModeCodeDO
    {
        public string Note { get; set; }

        public NomValue Type { get; set; }

        public string CodeHex { get; set; }

        public int? AircraftId { get; set; }

        public string TheirNumber { get; set; }

        public DateTime? TheirDate { get; set; }

        public string CaaNumber { get; set; }

        public DateTime? CaaDate { get; set; }

        public bool ApplicantIsOrg { get; set; }

        public NomValue ApplicantPerson { get; set; }

        public NomValue ApplicantOrganization { get; set; }

        public NomValue Valid { get; set; }
    }
}
