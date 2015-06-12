using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;
using Gva.Api.Models.Views.SModeCode;

namespace Gva.Api.ModelsDO.SModeCodes
{
    public class SModeCodeViewDO
    {
        public SModeCodeViewDO(GvaViewSModeCode code)
        {
            int codeHexToDecimal = Convert.ToInt32(code.CodeHex, 16);
            this.LotId = code.LotId;
            this.CodeHex = code.CodeHex;
            this.CodeDecimal = codeHexToDecimal;
            this.CodeBinary = Convert.ToString(codeHexToDecimal, 2);
            this.Type = code.Type;
            this.Description = code.Description;
            this.Identifier = code.Identifier;
            this.RegMark = code.RegMark;
        }

        public int LotId { get; set; }

        public string Description { get; set; }

        public string Identifier { get; set; }

        public string RegMark { get; set; }

        public NomValue Type { get; set; }

        public string CodeHex { get; set; }

        public int CodeDecimal { get; set; }

        public string CodeBinary { get; set; }
    }
}
