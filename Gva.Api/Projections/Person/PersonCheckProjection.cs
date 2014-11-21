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
    public class PersonCheckProjection : Projection<GvaViewPersonCheck>
    {
        private IUnitOfWork unitOfWork;

        public PersonCheckProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
            this.unitOfWork = unitOfWork;
        }

        public override IEnumerable<GvaViewPersonCheck> Execute(PartCollection parts)
        {
            var checks = parts.GetAll<PersonCheckDO>("personDocumentChecks");

            return checks.Select(c => this.Create(c));
        }

        private GvaViewPersonCheck Create(PartVersion<PersonCheckDO> personCheck)
        {
            GvaViewPersonCheck check = new GvaViewPersonCheck();

            check.PersonLin = this.unitOfWork.DbContext.Set<GvaViewPerson>().Where(p => p.LotId == personCheck.Part.LotId).Single().Lin;
            check.LotId = personCheck.Part.LotId;
            check.PartId = personCheck.Part.PartId;
            check.PartIndex = personCheck.Part.Index;
            check.Publisher = personCheck.Content.DocumentPublisher;
            check.DocumentNumber = personCheck.Content.DocumentNumber;
            check.DocumentTypeId = personCheck.Content.DocumentType != null ? personCheck.Content.DocumentType.NomValueId : (int?)null;
            check.DocumentRoleId = personCheck.Content.DocumentRole != null ? personCheck.Content.DocumentRole.NomValueId : (int?)null;
            check.RatingTypeId = personCheck.Content.RatingType != null ? personCheck.Content.RatingType.NomValueId : (int?)null;
            check.RatingClassId = personCheck.Content.RatingClass != null ? personCheck.Content.RatingClass.NomValueId : (int?)null;
            check.AuthorizationId = personCheck.Content.Authorization != null ? personCheck.Content.Authorization.NomValueId : (int?)null;
            check.LicenceTypeId = personCheck.Content.LicenceType != null ? personCheck.Content.LicenceType.NomValueId : (int?)null;
            check.ValidId = personCheck.Content.Valid != null ? personCheck.Content.Valid.NomValueId : (int?)null;
            check.PersonCheckRatingValueId = personCheck.Content.PersonCheckRatingValue != null ? personCheck.Content.PersonCheckRatingValue.NomValueId : (int?)null;
            check.Sector = personCheck.Content.Sector;

            if(personCheck.Content.DocumentDateValidFrom.HasValue)
            {
                check.DocumentDateValidFrom = personCheck.Content.DocumentDateValidFrom.Value;
            }
            if(personCheck.Content.DocumentDateValidTo.HasValue)
            {
                check.DocumentDateValidTo = personCheck.Content.DocumentDateValidTo.Value;
            }

            return check;
        }
    }
}
