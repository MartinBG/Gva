using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.InventoryView
{
    public class MedicalHandler : CommitEventHandler<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public MedicalHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "medical",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
            this.userRepository = userRepository;
        }

        public override void Fill(GvaViewInventoryItem invItem, PartVersion partVersion)
        {
            invItem.Lot = partVersion.Part.Lot;
            invItem.Part = partVersion.Part;

            invItem.DocumentType = partVersion.Part.SetPart.Alias;
            invItem.Name = partVersion.Part.SetPart.Name;
            invItem.Type = null;
            invItem.Number = string.Format(
                    "{0}-{1}-{2}-{3}",
                    partVersion.DynamicContent.documentNumberPrefix,
                    partVersion.DynamicContent.documentNumber,
                    partVersion.Part.Lot.GetPart("personData").DynamicContent.lin,
                    partVersion.DynamicContent.documentNumberSuffix);
            invItem.Date = partVersion.DynamicContent.documentDateValidFrom;
            invItem.Publisher = partVersion.DynamicContent.documentPublisher.name;
            invItem.Valid = null;
            invItem.FromDate = partVersion.DynamicContent.documentDateValidFrom;
            invItem.ToDate = partVersion.DynamicContent.documentDateValidTo;

            if (partVersion.PartOperation == PartOperation.Add)
            {
                invItem.CreatedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname;
                invItem.CreationDate = partVersion.CreateDate;
            }
            else
            {
                invItem.EditedBy = this.userRepository.GetUser(partVersion.CreatorId).Fullname;
                invItem.EditedDate = partVersion.CreateDate;
            }
        }

        public override void Clear(GvaViewInventoryItem person)
        {
            throw new NotSupportedException();
        }
    }
}
