using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.Api.Models.Views.SModeCode;
using Gva.Api.ModelsDO.SModeCodes;

namespace Gva.Api.Repositories.SModeCodeRepository
{
    public interface ISModeCodeRepository
    {
        IEnumerable<GvaViewSModeCode> GetSModeCodes(
            int? typeId = null,
            string codeHex = null,
            string regMark = null,
            string identifier = null,
            int? isConnectedToRegistrationId = null,
            int offset = 0,
            int? limit = null);

        string GetNextHexCode(int typeId);
    }
}
