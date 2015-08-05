using System;
using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.Repositories.PersonLangCertRepository
{
    public interface IPersonLangCertRepository
    {
        List<PersonLangCertViewDO> GetLangCerts(int lotId, int? caseTypeId = null, bool? valid = null);
    }
}
