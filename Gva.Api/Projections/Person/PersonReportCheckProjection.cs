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
    public class PersonReportCheckProjection : Projection<GvaViewPersonReportCheck>
    {
        private IUnitOfWork unitOfWork;

        public PersonReportCheckProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
            this.unitOfWork = unitOfWork;
        }

        public override IEnumerable<GvaViewPersonReportCheck> Execute(PartCollection parts)
        {
            var reports = parts.GetAll<PersonReportDO>("personReports");

            List<GvaViewPersonReportCheck> checksToReports = new List<GvaViewPersonReportCheck>();
            foreach (var report in reports)
            {
                foreach (int checkPartId in report.Content.IncludedChecks)
                {
                    var check = this.unitOfWork.DbContext.Set<GvaViewPersonCheck>().Where(c => c.PartId == checkPartId).Single();
                    checksToReports.Add(
                        new GvaViewPersonReportCheck()
                        {
                            CheckLotId = check.LotId,
                            CheckPartIndex = check.PartIndex,
                            LotId = report.Part.Lot.LotId,
                            PartIndex = report.Part.Index
                        });
                }
            }

            return checksToReports;
        }
    }
}
