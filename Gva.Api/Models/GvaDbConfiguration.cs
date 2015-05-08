using System.Data.Entity;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.Models.Views.Aircarft;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.Models.Views.Airport;
using Gva.Api.Models.Views.Equipment;
using Gva.Api.Models.Views.Organization;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models
{
    public class GvaDbConfiguration : IDbConfiguration
    {
        public void AddConfiguration(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new GvaCaseTypeMap());
            modelBuilder.Configurations.Add(new GvaCorrespondentMap());
            modelBuilder.Configurations.Add(new GvaLotCaseMap());
            modelBuilder.Configurations.Add(new GvaApplicationMap());
            modelBuilder.Configurations.Add(new GvaApplicationStageMap());
            modelBuilder.Configurations.Add(new GvaStageMap());
            modelBuilder.Configurations.Add(new GvaAppLotFileMap());
            modelBuilder.Configurations.Add(new GvaFileMap());
            modelBuilder.Configurations.Add(new GvaLotFileMap());
            modelBuilder.Configurations.Add(new GvaViewPersonMap());
            modelBuilder.Configurations.Add(new GvaViewPersonInspectorMap());
            modelBuilder.Configurations.Add(new GvaViewPersonExaminerMap());
            modelBuilder.Configurations.Add(new GvaViewPersonLicenceMap());
            modelBuilder.Configurations.Add(new GvaViewPersonLicenceEditionMap());
            modelBuilder.Configurations.Add(new GvaViewPersonRatingMap());
            modelBuilder.Configurations.Add(new GvaViewPersonRatingEditionMap());
            modelBuilder.Configurations.Add(new GvaViewPrintedRatingEditionMap());
            modelBuilder.Configurations.Add(new GvaViewPersonQualificationMap());
            modelBuilder.Configurations.Add(new GvaViewPersonApplicationExamMap());
            modelBuilder.Configurations.Add(new GvaLicenceEditionMap());
            modelBuilder.Configurations.Add(new GvaViewPersonDocumentMap());
            modelBuilder.Configurations.Add(new GvaViewPersonCheckMap());
            modelBuilder.Configurations.Add(new GvaViewPersonReportMap());
            modelBuilder.Configurations.Add(new GvaViewPersonReportCheckMap());
            modelBuilder.Configurations.Add(new GvaViewInventoryItemMap());
            modelBuilder.Configurations.Add(new GvaViewApplicationMap());
            modelBuilder.Configurations.Add(new GvaViewOrganizationMap());
            modelBuilder.Configurations.Add(new GvaViewOrganizationRecommendationMap());
            modelBuilder.Configurations.Add(new GvaViewOrganizationInspectionMap());
            modelBuilder.Configurations.Add(new GvaViewOrganizationInspectionRecommendationMap());
            modelBuilder.Configurations.Add(new GvaViewOrganizationApprovalMap());
            modelBuilder.Configurations.Add(new GvaViewOrganizationAmendmentMap());
            modelBuilder.Configurations.Add(new GvaViewAircraftMap());
            modelBuilder.Configurations.Add(new GvaViewAirportMap());
            modelBuilder.Configurations.Add(new GvaViewAircraftRegistrationMap());
            modelBuilder.Configurations.Add(new GvaViewAircraftRegMarkMap());
            modelBuilder.Configurations.Add(new GvaViewAircraftCertAirworthinessMap());
            modelBuilder.Configurations.Add(new GvaViewAircraftPrintedCertNoiseMap());
            modelBuilder.Configurations.Add(new GvaInvalidActNumberMap());
            modelBuilder.Configurations.Add(new GvaViewEquipmentMap());
            modelBuilder.Configurations.Add(new GvaWordTemplateMap());
            modelBuilder.Configurations.Add(new ASExamVariantQuestionMap());
            modelBuilder.Configurations.Add(new ASExamVariantMap());
            modelBuilder.Configurations.Add(new ASExamQuestionMap());
            modelBuilder.Configurations.Add(new GvaExSystCertCampaignMap());
            modelBuilder.Configurations.Add(new GvaExSystCertPathMap());
            modelBuilder.Configurations.Add(new GvaExSystExamineeMap());
            modelBuilder.Configurations.Add(new GvaExSystQualificationMap());
            modelBuilder.Configurations.Add(new GvaExSystExamMap());
        }
    }
}
