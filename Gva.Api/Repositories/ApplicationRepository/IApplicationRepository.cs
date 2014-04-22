using System;
using System.Collections.Generic;
using Common.Api.Repositories;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

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

        GvaApplication GetNomApplication(int applicationId);

        IEnumerable<GvaApplication> GetLinkedToDocsApplications();

        void AddGvaApplication(GvaApplication gvaApplication);

        void DeleteGvaApplication(int gvaAppLotPartId);

        void AddGvaLotFile(GvaLotFile gvaLotFile);

        void AddGvaAppLotFile(GvaAppLotFile gvaAppLotFile);

        GvaCorrespondent GetGvaCorrespondentByLotId(int lotId);

        void AddGvaCorrespondent(GvaCorrespondent gvaCorrespondent);

        GvaApplication GetGvaApplicationByDocId(int docId);

        void AddApplicationRefs(Part part, IEnumerable<ApplicationNomDO> applications);

        GvaApplication[] GetApplicationRefs(int partId);

        void DeleteApplicationRefs(PartVersion partVersion);

        IEnumerable<Set> GetLotSets(
            string name = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        Set GetLotSet(int lotSetId);
    }
}
