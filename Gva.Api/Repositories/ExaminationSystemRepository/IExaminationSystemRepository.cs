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
        void ExtractDataFromExaminationSystem(bool extractExaminees);

        List<GvaExSystCertCampaignDO> GetCertCampaigns(string code = null);

        List<GvaExSystCertPathDO> GetCertPaths();

        List<GvaExSystTestDO> GetTests(string qualificationCode = null, string certCampCode = null, string testCode = null, int? certPathCode = null);

        List<GvaExSystExamineeDO> GetExaminees();

        List<GvaExSystQualification> GetQualifications(string qualificationCode = null);

        void ReloadStates();
    }
}
