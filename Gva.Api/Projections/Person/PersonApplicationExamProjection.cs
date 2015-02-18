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
    public class PersonApplicationExamProjection : Projection<GvaViewPersonApplicationExam>
    {
        public PersonApplicationExamProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonApplicationExam> Execute(PartCollection parts)
        {
            var applications = parts.GetAll<DocumentApplicationDO>("personDocumentApplications");

            List<GvaViewPersonApplicationExam> result = new List<GvaViewPersonApplicationExam>();
            foreach (var application in applications.Where(
                a => a.Content.ExaminationSystemData != null &&
                a.Content.ExaminationSystemData.Exams != null && 
                a.Content.ExaminationSystemData.Exams.Count() > 0))
            {
                List<GvaViewPersonApplicationExam> exams = application.Content.ExaminationSystemData.Exams
                    .Select(t =>
                        this.Create(t, application.PartId, application.Part.LotId, application.Content.ExaminationSystemData.CertCampaign))
                    .ToList();
                result = result.Union(exams)
                    .ToList();
            }

            return result;
        }

        private GvaViewPersonApplicationExam Create(AppExamSystExamDO exam, int applicationPartId, int lotId, NomValue certCampaign)
        {
            GvaViewPersonApplicationExam applicationExam = new GvaViewPersonApplicationExam();
            applicationExam.AppPartId = applicationPartId;
            applicationExam.LotId = lotId;
            applicationExam.ExamCode = exam.ExamData.Code;
            applicationExam.ExamName = exam.ExamData.Name;
            applicationExam.ExamDate = exam.Date;
            applicationExam.CertCampCode = certCampaign.Code;
            applicationExam.CertCampName = certCampaign.Name;

            return applicationExam;
        }
    }
}
