using System;
using System.Collections.Generic;
using Common.Api.Repositories;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public interface IApplicationRepository : IRepository<GvaApplication>
    {
        IEnumerable<ApplicationListDO> GetApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string lin = null,
            int offset = 0,
            int? limit = null
            );

        GvaApplication[] GetNomApplications(int lotId);

        IEnumerable<GvaApplication> GetLinkedToDocsApplications();

        void AddGvaApplication(GvaApplication gvaApplication);

        void DeleteGvaApplication(int gvaAppLotPartId);

        void AddGvaLotFile(GvaLotFile gvaLotFile);

        void AddGvaAppLotFile(GvaAppLotFile gvaAppLotFile);

        GvaCorrespondent GetGvaCorrespondentByLotId(int lotId);

        void AddGvaCorrespondent(GvaCorrespondent gvaCorrespondent);
    }
}
