using System.Collections.Generic;
using Gva.Api.Models;
using System;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public interface IApplicationRepository
    {
        IEnumerable<ApplicationListDO> GetApplications(DateTime? fromDate, DateTime? toDate, string lin);

        GvaApplication[] GetNomApplications(int lotId);

        void AddGvaApplication(GvaApplication gvaApplication);

        void DeleteGvaApplication(int gvaAppLotPartId);

        GvaApplicationSearch GetGvaApplicationSearch(int lotPartId);

        void AddGvaApplicationSearch(GvaApplicationSearch gvaApplicationSearch);

        void DeleteGvaApplicationSearch(int lotPartId);
    }
}
