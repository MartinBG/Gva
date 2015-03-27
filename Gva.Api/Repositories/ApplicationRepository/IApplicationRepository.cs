using System;
using System.Collections.Generic;
using Common.Api.Repositories;
using Gva.Api.Models;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.ModelsDO.Common;
using Regs.Api.Models;

namespace Gva.Api.Repositories.ApplicationRepository
{
    public interface IApplicationRepository : IRepository<GvaApplication>
    {
        ApplicationsDO GetApplications(
            string lotSetAlias = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? personLin = null,
            string aircraftIcao = null,
            string organizationUin = null,
            int? stageId = null,
            int? inspectorId = null,
            int offset = 0,
            int? limit = null);

        GvaViewApplication GetApplicationById(int applicationId);

        GvaViewApplication GetApplicationByPartId(int applicationPartId);

        IEnumerable<ApplicationExamListDO> GetPersonApplicationExams(int offset = 0, int? limit = null);

        ApplicationsDO GetPersonApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? personLin = null,
            int? stageId = null,
            int? inspectorId = null,
            int offset = 0,
            int? limit = null);

        ApplicationsDO GetAircraftApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string aircraftIcao = null,
            int? stageId = null,
            int? inspectorId = null,
            int offset = 0,
            int? limit = null);

        ApplicationsDO GetOrganizationApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string organizationUin = null,
            int? stageId = null,
            int? inspectorId = null,
            int offset = 0,
            int? limit = null);

        ApplicationsDO GetEquipmentApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? stageId = null,
            int? inspectorId = null,
            int offset = 0,
            int? limit = null);

        ApplicationsDO GetAirportApplications(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? stageId = null,
            int? inspectorId = null,
            int offset = 0,
            int? limit = null);

        GvaApplication[] GetNomApplications(int lotId);

        GvaApplication GetNomApplication(int applicationId);

        IEnumerable<GvaApplication> GetLinkedToDocsApplications();

        void AddGvaApplication(GvaApplication gvaApplication);

        void DeleteGvaApplication(int gvaAppLotPartId);

        void AddGvaLotFile(GvaLotFile gvaLotFile);

        void AddGvaAppLotFile(GvaAppLotFile gvaAppLotFile);

        List<GvaCorrespondent> GetGvaCorrespondentsByLotId(int lotId);

        void AddGvaCorrespondent(GvaCorrespondent gvaCorrespondent);

        GvaApplication GetGvaApplicationByDocId(int docId);

        IEnumerable<Set> GetLotSets(
            string name = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        Set GetLotSet(int lotSetId);

        ApplicationNomDO GetInitApplication(int? applicationId);

        List<CaseTypePartDO<DocumentApplicationDO>> GetApplicationsForLot(int lotId, string path, int? caseTypeId = null);

        CaseTypePartDO<DocumentApplicationDO> GetApplicationPart(string path, int lotId);

        List<AppExamSystQualificationDO> GetApplicationQualifications(string path, int lotId);

        ApplicationMainDO CreateNewApplication(ApplicationNewDO applicationNewDO, string docRegUri = null);
    }
}
