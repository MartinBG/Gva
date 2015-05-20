using Common.Api.Models;
using Common.Data;
using Docs.Api.Models.DomainModels;
using Docs.Api.Models.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.Repositories.UnitRepository
{
    class UnitUserRepository : IUnitUserRepository
    {
        private IUnitOfWork unitOfWork;

        public UnitUserRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UserForUnitAttachmentDomainModel> GetUsersNotAttachedToUnit()
        {
            var users = unitOfWork.DbContext.Set<User>();
            var unitUsers = unitOfWork.DbContext.Set<UnitUser>();

            var result = users.Where(e =>
                !unitUsers.Any(uu =>
                    uu.UserId == e.UserId
                    && uu.IsActive)
                && e.IsActive)
                .Select(e => new UserForUnitAttachmentDomainModel {
                    UserId = e.UserId,
                    UserName = e.Username,
                    FullName = e.Fullname
                });

            return result;
        }

        public void AttachUnitToUser(int unitId, int userId)
        {
            var unitUsers = unitOfWork.DbContext.Set<UnitUser>();
            var unitUser = new UnitUser {
                UnitId = unitId,
                UserId = userId,
                IsActive = true
            };

            unitUsers.Add(unitUser);
            unitOfWork.Save();
        }

        public void DeactivateUniUserRelation(int unitId, int userId)
        {
            var unitUsers = unitOfWork.DbContext.Set<UnitUser>();
            var unitUser = unitUsers.Single(e =>
                e.UnitId == unitId
                && e.UserId == userId
                && e.IsActive);

            unitUser.IsActive = false;
            unitOfWork.Save();
        }
    }
}
