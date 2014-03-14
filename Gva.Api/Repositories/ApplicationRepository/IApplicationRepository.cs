using System.Collections.Generic;
using Gva.Api.Models;
using System;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public interface IApplicationRepository
    {
        IEnumerable<GvaApplication> GetApplications(DateTime? fromDate, DateTime? toDate, string lin, string regUri);

        GvaApplication GetApplication(int applicationId);

        void AddApplication(GvaApplication application);
    }
}
