using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.Enums
{
    public enum DomainErrorCode
    {
        // Enum order does not matter, as value is not tacken into account
        Entity_NotFound,
        Entity_NotFoundOrNotActive,
        Entity_AlreadyExistInRelation,
        Entity_CannotBeDeactivated,
        Entity_CannotBeActivated,
        Unit_NotFound,
        Unit_NotFoundOrNotActive,
        Unit_UnitTypeIsNotUser,
        Unit_ErrorWithParameters,
        Unit_NotFound_ActivOfTypeEmployee,
        Unit_CannotBeDeleted_ExistingRelation
    }
}
