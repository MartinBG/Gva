using Docs.Api.Models.DomainModels;
using System.Collections.Generic;

namespace Docs.Api.Repositories.UnitRepository
{
    public interface IUnitUserRepository
    {
        IEnumerable<UserForUnitAttachmentDomainModel> GetUsersNotAttachedToUnit();

        void AttachUnitToUser(int unitId, int userId);

        void DeactivateUniUserRelation(int unitId, int userId);
    }
}
