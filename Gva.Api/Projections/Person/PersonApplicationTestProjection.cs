using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Projections.Person
{
    public class PersonApplicationTestProjection : Projection<GvaViewPersonApplicationTest>
    {
        public PersonApplicationTestProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonApplicationTest> Execute(PartCollection parts)
        {
            var applications = parts.GetAll<DocumentApplicationDO>("personDocumentApplications");
            List<GvaViewPersonApplicationTest> result = new List<GvaViewPersonApplicationTest>();
            foreach (var application in applications.Where(a => a.Content.ExaminationSystemData != null && a.Content.ExaminationSystemData.Tests.Count() > 0))
            {
                List<GvaViewPersonApplicationTest> tests = application.Content.ExaminationSystemData.Tests
                    .Select(t =>
                        this.Create(t, application.Content.ApplicationId, application.Part.LotId, application.Content.ExaminationSystemData.CertCampaign))
                    .ToList();
                result = result.Union(tests)
                    .ToList();
            }

            return result;
        }


        private GvaViewPersonApplicationTest Create(AppExamSystTestDO test, int applicationId, int lotId, NomValue certCampaign)
        {
            GvaViewPersonApplicationTest applicationTest = new GvaViewPersonApplicationTest();
            applicationTest.GvaApplicationId = applicationId;
            applicationTest.LotId = lotId;
            applicationTest.TestCode = test.TestData.Code;
            applicationTest.TestName = test.TestData.Name;
            applicationTest.TestDate = test.Date;
            applicationTest.CertCampCode = certCampaign.Code;
            applicationTest.CertCampName = certCampaign.Name;

            return applicationTest;
        }
    }
}
