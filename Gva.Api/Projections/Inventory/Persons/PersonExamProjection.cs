using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Persons
{
    public class PersonExamProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;
        private INomRepository nomRepository;

        public PersonExamProjection(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var exams = parts.GetAll<PersonDocumentExamDO>("personDocumentExams");

            return exams.Select(e => this.Create(e));
        }

        private GvaViewInventoryItem Create(PartVersion<PersonDocumentExamDO> personExam)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();
            var role = this.nomRepository.GetNomValue("documentRoles", "exam");

            invItem.LotId = personExam.Part.Lot.LotId;
            invItem.PartId = personExam.Part.PartId;
            invItem.SetPartAlias = personExam.Part.SetPart.Alias;
            invItem.Name = role.Name;
            invItem.TypeId = personExam.Content.DocumentType.NomValueId;
            invItem.Number = personExam.Content.DocumentNumber;
            invItem.Date = personExam.Content.DocumentDateValidFrom.Value;
            invItem.Publisher = personExam.Content.DocumentPublisher;
            invItem.Valid = personExam.Content.Valid.Code == "Y";
            invItem.FromDate = personExam.Content.DocumentDateValidFrom.Value;
            invItem.ToDate = personExam.Content.DocumentDateValidTo;
            invItem.Notes = personExam.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(personExam.Part.CreatorId).Fullname;
            invItem.CreationDate = personExam.Part.CreateDate;

            if (personExam.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personExam.CreatorId).Fullname;
                invItem.EditedDate = personExam.CreateDate;
            }

            return invItem;
        }
    }
}
