using System;
using System.Collections.Generic;
using Common.Api.Repositories;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public interface IApplicationRepository : IRepository<GvaApplication>
    {
        IEnumerable<ApplicationListDO> GetApplications(DateTime? fromDate, DateTime? toDate, string lin);

        GvaApplication[] GetNomApplications(int lotId);

        IEnumerable<GvaApplication> GetLinkedToDocsApplications();

        void AddGvaApplication(GvaApplication gvaApplication);

        void DeleteGvaApplication(int gvaAppLotPartId);

        void AddGvaLotFile(GvaLotFile gvaLotFile);

        void AddGvaAppLotFile(GvaAppLotFile gvaAppLotFile);
    }
}
