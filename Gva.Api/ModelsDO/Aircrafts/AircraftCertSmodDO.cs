using System;
using Common.Api.Models;
using Gva.Api.Models.Views.SModeCode;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertSmodDO
    {
        public AircraftCertSmodDO(GvaViewSModeCode code)
        {
            int codeHexToDecimal = Convert.ToInt32(code.CodeHex, 16);
            this.TheirDate = code.TheirDate;
            this.TheirNumber = code.TheirNumber;
            this.CaaDate = code.CaaDate;
            this.CaaNumber = code.CaaNumber;
            this.SModeCodeLotId = code.LotId;
            this.CodeHex = code.CodeHex;
            this.CodeDecimal = codeHexToDecimal;
            this.CodeBinary = Convert.ToString(codeHexToDecimal, 2);
        }

        public int SModeCodeLotId { get; set; } 

        public string TheirNumber { get; set; }

        public DateTime? TheirDate { get; set; }

        public string CaaNumber { get; set; }

        public DateTime? CaaDate { get; set; }

        public string CodeHex { get; set; }

        public int CodeDecimal { get; set; }

        public string CodeBinary { get; set; }
    }
}
