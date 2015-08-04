using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
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
        private INomRepository nomRepository;

        public PersonCheckProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.unitOfWork = unitOfWork;
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonCheck> Execute(PartCollection parts)
        {
            var checks = parts.GetAll<PersonCheckDO>("personDocumentChecks");

            return checks.Select(c => this.Create(c));
        }

        private GvaViewPersonCheck Create(PartVersion<PersonCheckDO> personCheck)
        {
            GvaViewPersonCheck check = new GvaViewPersonCheck();

            check.LotId = personCheck.Part.LotId;
            check.PartId = personCheck.Part.PartId;
            check.PartIndex = personCheck.Part.Index;
            check.Publisher = personCheck.Content.DocumentPublisher;
            check.DocumentNumber = personCheck.Content.DocumentNumber;
            check.DocumentTypeId = personCheck.Content.DocumentTypeId;
            check.DocumentRoleId = personCheck.Content.DocumentRoleId;
            check.RatingTypes = personCheck.Content.RatingTypes.Count() > 0 ? string.Join(", ", personCheck.Content.RatingTypes.Select(r => this.nomRepository.GetNomValue("ratingTypes", r).Code)) : null;
            check.RatingClassId = personCheck.Content.RatingClassId;
            check.AuthorizationId = personCheck.Content.AuthorizationId;
            check.LicenceTypeId = personCheck.Content.LicenceTypeId;
            check.ValidId = personCheck.Content.ValidId;
            check.PersonCheckRatingValueId = personCheck.Content.PersonCheckRatingValueId;
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
