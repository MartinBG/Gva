using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Projections.Person
{
    public class PersonReportProjection : Projection<GvaViewPersonReport>
    {
        private IUnitOfWork unitOfWork;

        public PersonReportProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
            this.unitOfWork = unitOfWork;
        }

        public override IEnumerable<GvaViewPersonReport> Execute(PartCollection parts)
        {
            var reports = parts.GetAll<PersonReportDO>("personReports");

            return reports.Select(c => this.Create(c));
        }

        private GvaViewPersonReport Create(PartVersion<PersonReportDO> personReport)
        {
            GvaViewPersonReport report = new GvaViewPersonReport();
            report.LotId = personReport.Part.LotId;
            report.PartIndex = personReport.Part.Index;
            report.DocumentNumber = personReport.Content.DocumentNumber;
            report.Date = personReport.Content.Date;

            var person = this.unitOfWork.DbContext.Set<GvaViewPerson>().Where(p => p.LotId == personReport.Part.LotId).Single();
            report.Publisher = string.Format("{0} {1}", person.Lin, person.Names);

            return report;
        }
    }
}
