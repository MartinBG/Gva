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
    //? rewrite with predicate
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

        public Correspondent GetBgCitizenCorrespondent(string email, string bgCitizenFirstName, string bgCitizenLastName, string BgCitizenUin)
        {
            return this.unitOfWork.DbContext.Set<Correspondent>()
                .FirstOrDefault(c =>
                    c.CorrespondentType.Alias == "BulgarianCitizen" &&
                    email != null ? c.Email == email : c.Email == null &&
                    bgCitizenFirstName != null ? c.BgCitizenFirstName == bgCitizenFirstName : c.BgCitizenFirstName == null &&
                    bgCitizenLastName != null ? c.BgCitizenLastName == bgCitizenLastName : c.BgCitizenLastName == null &&
                    BgCitizenUin != null ? c.BgCitizenUIN == BgCitizenUin : c.BgCitizenUIN == null);
        }

        public Correspondent GetForeignerCorrespondent(string email, string foreignerFirstName, string foreignerLastName, int? foreignerCountryId, string foreignerSettlement, DateTime? foreignerBirthDate)
        {
            return this.unitOfWork.DbContext.Set<Correspondent>()
                .FirstOrDefault(c =>
                    c.CorrespondentType.Alias == "Foreigner" &&
                    email != null ? c.Email == email : c.Email == null &&
                    foreignerFirstName != null ? c.ForeignerFirstName == foreignerFirstName : c.ForeignerFirstName == null &&
                    foreignerLastName != null ? c.ForeignerLastName == foreignerLastName : c.ForeignerLastName == null &&
                    c.ForeignerCountryId == foreignerCountryId &&
                    foreignerSettlement != null ? c.ForeignerSettlement == foreignerSettlement : c.ForeignerSettlement == null &&
                    foreignerBirthDate.HasValue ? c.ForeignerBirthDate == foreignerBirthDate.Value : c.ForeignerBirthDate == null);
        }

        public Correspondent GetLegalEntityCorrespondent(string email, string LegalEntityName, string LegalEntityBulstat)
        {
            return this.unitOfWork.DbContext.Set<Correspondent>()
                .FirstOrDefault(c =>
                    c.CorrespondentType.Alias == "LegalEntity" &&
                    LegalEntityName != null ? c.LegalEntityName == LegalEntityName : c.LegalEntityName == null &&
                    LegalEntityBulstat != null ? c.LegalEntityBulstat == LegalEntityBulstat : c.LegalEntityBulstat == null &&
                    email != null ? c.Email == email : c.Email == null);
        }

        public Correspondent GetFLegalEntityCorrespondent(string email, string fLegalEntityName, int? fLegalEntityCountryId, string fLegalEntityRegisterName, string fLegalEntityRegisterNumber)
        {
            return this.unitOfWork.DbContext.Set<Correspondent>()
                .FirstOrDefault(c =>
                    c.CorrespondentType.Alias == "ForeignLegalEntity" &&
                    email != null ? c.Email == email : c.Email == null &&
                    fLegalEntityName != null ? c.FLegalEntityName == fLegalEntityName : c.FLegalEntityName == null &&
                    c.FLegalEntityCountryId == fLegalEntityCountryId &&
                    fLegalEntityRegisterName != null ? c.FLegalEntityRegisterName == fLegalEntityRegisterName : c.FLegalEntityRegisterName == null &&
                    fLegalEntityRegisterNumber != null ? c.FLegalEntityRegisterNumber == fLegalEntityRegisterNumber : c.FLegalEntityRegisterNumber == null);
        }
    }
}
