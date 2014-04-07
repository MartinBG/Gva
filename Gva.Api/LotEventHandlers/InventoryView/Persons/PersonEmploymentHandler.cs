using System;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.InventoryView
{
    public class PersonEmploymentHandler : CommitEventHandler<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonEmploymentHandler(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "personEmployment",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
            this.userRepository = userRepository;
        }

        public override void Fill(GvaViewInventoryItem invItem, PartVersion partVersion)
        {
            invItem.Lot = partVersion.Part.Lot;
            invItem.Part = partVersion.Part;

            invItem.SetPartAlias = partVersion.Part.SetPart.Alias;
            invItem.Name = partVersion.Part.SetPart.Name;
            invItem.Type = partVersion.DynamicContent.employmentCategory.name;
            invItem.Number = null;
            invItem.Date = partVersion.DynamicContent.hiredate;
            invItem.Publisher = partVersion.DynamicContent.organization == null ? null : partVersion.DynamicContent.organization.name;
            invItem.Valid = partVersion.DynamicContent.valid.code == "Y";
            invItem.FromDate = partVersion.DynamicContent.hiredate;
            invItem.ToDate = null;

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
