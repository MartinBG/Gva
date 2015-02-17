using System.Collections.Generic;
using System.Web.Http;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.ExaminationSystem;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;

namespace Gva.Api.Repositories.ExaminationSystemRepository
{
    public interface IExaminationSystemRepository
    {
        void ExtractDataFromExaminationSystem();

        List<GvaExSystCertCampaignDO> GetCertCampaigns(string code = null);

        List<GvaExSystCertPathDO> GetCertPaths();

        List<GvaExSystExamDO> GetExams(string qualificationCode = null, string certCampCode = null, string examCode = null, int? certPathCode = null);

        List<GvaExSystExamineeDO> GetExaminees(int? lotId = null);

        List<GvaExSystQualification> GetQualifications(string qualificationCode = null);

        void SaveNewState(int lotId, PersonNewExamSystStateDO state);

        void ReloadStates();
    }
}
