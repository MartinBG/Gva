using Common.Data;
using Docs.Api.Models.ClassificationModels;
using Docs.Api.Models.DomainModels;
using Docs.Api.Models.UnitModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.Repositories.UnitRepository
{
    class UnitClassificationRepository : IUnitClassificationRepository
    {
        private IUnitOfWork unitOfWork;

        public UnitClassificationRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<UnitClassificationDomainModel> GetClassificationsForUnit(int unitId)
        {
            var result = unitOfWork.DbContext.Set<UnitClassification>()
                .Join(unitOfWork.DbContext.Set<Classification>(),
                unitClassification => unitClassification.ClassificationId,
                classification => classification.ClassificationId,
                (unitClassification, classification) => new UnitClassificationDomainModel
                {
                    UnitClassificationId = unitClassification.UnitClassificationId,
                    ClassificationName = classification.Name,
                    ClassificationPermission = ((ClassificationPermissionType)unitClassification.ClassificationPermissionId).ToString()
                });

            return result;
        }
    }
}
