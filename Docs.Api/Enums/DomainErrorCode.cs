using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.Enums
{
    public enum DomainErrorCode
    {
        Entity_NotFound,
        Entity_NotFoundOrNotActive,
        Entity_AlreadyExistInRelation,
        Unit_NotFound,
        Unit_NotFoundOrNotActive,
        Unit_UnitTypeIsNotUser,
        Unit_ErrorWithParameters,
        Unit_NotFound_ActivOfTypeEmployee
    }
}
