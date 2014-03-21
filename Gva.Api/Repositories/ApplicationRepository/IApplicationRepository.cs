using System.Collections.Generic;
using Gva.Api.Models;
using System;
using Gva.Api.ModelsDO;
using Common.Api.Repositories;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public interface IApplicationRepository : IRepository<GvaApplication>
    {
        IEnumerable<ApplicationListDO> GetApplications(DateTime? fromDate, DateTime? toDate, string lin);

        GvaApplication[] GetNomApplications(int lotId);

        IEnumerable<GvaApplication> GetLinkedToDocsApplications();

        void AddGvaApplication(GvaApplication gvaApplication);

        void DeleteGvaApplication(int gvaAppLotPartId);

        GvaApplicationSearch GetGvaApplicationSearch(int lotPartId);

        void AddGvaApplicationSearch(GvaApplicationSearch gvaApplicationSearch);

        void DeleteGvaApplicationSearch(int lotPartId);

        void AddGvaLotFile(GvaLotFile gvaLotFile);

        void AddGvaAppLotFile(GvaAppLotFile gvaAppLotFile);
    }
}
