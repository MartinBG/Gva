using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Docs.Api.Models;
using Common.Api.UserContext;
using Common.Api.Repositories;

namespace Docs.Api.Repositories.CorrespondentRepository
{
    public class CorrespondentRepository : Repository<Correspondent>, ICorrespondentRepository
    {
        public CorrespondentRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<Correspondent> GetCorrespondents(
            string displayName,
            string correspondentEmail,
            int limit,
            int offset,
            out int totalCount)
        {
            var query =
               this.unitOfWork.DbContext.Set<Correspondent>()
               .Include(e => e.RegisterIndex)
               .Include(e => e.CorrespondentType)
               .Where(e => e.IsActive);

            if (!string.IsNullOrEmpty(displayName))
            {
                query = query.Where(e => e.DisplayName.Contains(displayName));
            }

            if (!string.IsNullOrEmpty(correspondentEmail))
            {
                query = query.Where(e => e.Email.Contains(correspondentEmail));
            }

            totalCount = query.Count();

            return query
                .OrderByDescending(e => e.CorrespondentId)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        //? replace with Find(id, includes[])
        public Correspondent GetCorrespondent(int id)
        {
            return this.unitOfWork.DbContext.Set<Correspondent>()
                .Include(е => е.CorrespondentContacts)
                .Include(e => e.RegisterIndex)
                .Include(e => e.CorrespondentType)
                .FirstOrDefault(e => e.CorrespondentId == id);
        }

        public Correspondent CreateBgCitizen(
            int correspondentGroupId,
            int correspondentTypeId,
            bool isActive,
            string bgCitizenFirstName,
            string bgCitizenLastName,
            string bgCitizenUIN,
            UserContext userContext)
        {
            Correspondent correspondent = new Correspondent();
            correspondent.CorrespondentGroupId = correspondentGroupId;
            correspondent.CorrespondentTypeId = correspondentTypeId;
            correspondent.IsActive = isActive;
            correspondent.ModifyDate = DateTime.Now;
            correspondent.ModifyUserId = userContext.UserId;
            correspondent.BgCitizenFirstName = bgCitizenFirstName;
            correspondent.BgCitizenLastName = bgCitizenLastName;
            correspondent.BgCitizenUIN = bgCitizenUIN;

            this.unitOfWork.DbContext.Set<Correspondent>().Add(correspondent);

            return correspondent;
        }

        public Correspondent CreateForeigner(
            int correspondentGroupId,
            int correspondentTypeId,
            bool isActive,
            string foreignerFirstName,
            string foreignerLastName,
            int? foreignerCountryId,
            string foreignerSettlement,
            DateTime? foreignerBirthDate,
            UserContext userContext)
        {
            Correspondent correspondent = new Correspondent();
            correspondent.CorrespondentGroupId = correspondentGroupId;
            correspondent.CorrespondentTypeId = correspondentTypeId;
            correspondent.IsActive = isActive;
            correspondent.ModifyDate = DateTime.Now;
            correspondent.ModifyUserId = userContext.UserId;
            correspondent.ForeignerFirstName = foreignerFirstName;
            correspondent.ForeignerLastName = foreignerLastName;
            correspondent.ForeignerCountryId = foreignerCountryId;
            correspondent.ForeignerSettlement = foreignerSettlement;
            correspondent.ForeignerBirthDate = foreignerBirthDate;

            this.unitOfWork.DbContext.Set<Correspondent>().Add(correspondent);

            return correspondent;
        }

        public Correspondent CreateLegalEntity(
            int correspondentGroupId,
            int correspondentTypeId,
            bool isActive,
            string legalEntityName,
            string legalEntityBulstat,
            UserContext userContext)
        {
            Correspondent correspondent = new Correspondent();
            correspondent.CorrespondentGroupId = correspondentGroupId;
            correspondent.CorrespondentTypeId = correspondentTypeId;
            correspondent.IsActive = isActive;
            correspondent.ModifyDate = DateTime.Now;
            correspondent.ModifyUserId = userContext.UserId;
            correspondent.LegalEntityName = legalEntityName;
            correspondent.LegalEntityBulstat = legalEntityBulstat;

            this.unitOfWork.DbContext.Set<Correspondent>().Add(correspondent);

            return correspondent;
        }

        public Correspondent CreateFLegalEntity(
            int correspondentGroupId,
            int correspondentTypeId,
            bool isActive,
            string fLegalEntityName,
            int? fLegalEntityCountryId,
            string fLegalEntityRegisterName,
            string fLegalEntityRegisterNumber,
            string fLegalEntityOtherData,
            UserContext userContext)
        {
            Correspondent correspondent = new Correspondent();
            correspondent.CorrespondentGroupId = correspondentGroupId;
            correspondent.CorrespondentTypeId = correspondentTypeId;
            correspondent.IsActive = isActive;
            correspondent.ModifyDate = DateTime.Now;
            correspondent.ModifyUserId = userContext.UserId;
            correspondent.FLegalEntityName = fLegalEntityName;
            correspondent.FLegalEntityCountryId = fLegalEntityCountryId;
            correspondent.FLegalEntityRegisterName = fLegalEntityRegisterName;
            correspondent.FLegalEntityRegisterNumber = fLegalEntityRegisterNumber;
            correspondent.FLegalEntityOtherData = fLegalEntityOtherData;

            this.unitOfWork.DbContext.Set<Correspondent>().Add(correspondent);

            return correspondent;
        }

        public void DeteleCorrespondent(int id, byte[] corrVersion)
        {
            Correspondent correspondent = this.unitOfWork.DbContext.Set<Correspondent>()
                .FirstOrDefault(e => e.CorrespondentId == id);

            if (correspondent != null)
            {
                correspondent.EnsureForProperVersion(corrVersion);

                this.unitOfWork.DbContext.Set<Correspondent>().Remove(correspondent);
            }
            else
            {
                throw new Exception(string.Format("Correspondent with ID = {0} does not exist.", id));
            }
        }
    }
}
