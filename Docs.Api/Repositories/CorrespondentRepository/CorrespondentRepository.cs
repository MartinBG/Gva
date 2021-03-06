﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api.DataObjects;
using Docs.Api.Models;

namespace Docs.Api.Repositories.CorrespondentRepository
{
    //? rewrite with predicate
    public class CorrespondentRepository : Repository<Correspondent>, ICorrespondentRepository
    {
        public CorrespondentRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public CorrespondentDO GetNewCorrespondent()
        { 
            CorrespondentGroup correspondentGroup = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "Applicants".ToLower());

            CorrespondentType correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                    .SingleOrDefault(e => e.Alias.ToLower() == "LegalEntity".ToLower());

            District district = this.unitOfWork.DbContext.Set<District>()
                    .SingleOrDefault(e => e.Code2 == "22");

            Municipality municipality = this.unitOfWork.DbContext.Set<Municipality>()
                    .SingleOrDefault(e => e.Code2 == "2246");

            Settlement settlement = this.unitOfWork.DbContext.Set<Settlement>()
                    .SingleOrDefault(e => e.Code == "68134");

            CorrespondentDO returnValue = new CorrespondentDO
            {
                CorrespondentGroupId = correspondentGroup != null ? correspondentGroup.CorrespondentGroupId : (int?)null,
                CorrespondentTypeId = correspondentType != null ? correspondentType.CorrespondentTypeId : (int?)null,
                CorrespondentTypeAlias = correspondentType != null ? correspondentType.Alias : string.Empty,
                CorrespondentTypeName = correspondentType != null ? correspondentType.Name : string.Empty,
                ContactDistrictId = district != null ? district.DistrictId : (int?)null,
                ContactMunicipalityId = municipality != null ? municipality.MunicipalityId : (int?)null,
                ContactSettlementId = settlement != null ? settlement.SettlementId : (int?)null,
                IsActive = true
            };

            returnValue.SetupFlags();

            return returnValue;
        }

        public CorrespondentDO CreateCorrespondent(CorrespondentDO corr, UserContext userContext)
        {
            Correspondent newCorr;

            CorrespondentType correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                .SingleOrDefault(e => e.CorrespondentTypeId == corr.CorrespondentTypeId);

            switch (correspondentType.Alias)
            {
                case "BulgarianCitizen":
                    newCorr = this.CreateBgCitizen(
                        corr.CorrespondentGroupId.Value,
                        corr.CorrespondentTypeId.Value,
                        true,
                        corr.BgCitizenFirstName,
                        corr.BgCitizenLastName,
                        corr.BgCitizenUIN,
                        userContext);
                    break;
                case "Foreigner":
                    newCorr = this.CreateForeigner(
                        corr.CorrespondentGroupId.Value,
                        corr.CorrespondentTypeId.Value,
                        true,
                        corr.ForeignerFirstName,
                        corr.ForeignerLastName,
                        corr.ForeignerCountryId,
                        corr.ForeignerSettlement,
                        corr.ForeignerBirthDate,
                        userContext);
                    break;
                case "LegalEntity":
                    newCorr = this.CreateLegalEntity(
                       corr.CorrespondentGroupId.Value,
                       corr.CorrespondentTypeId.Value,
                       true,
                       corr.LegalEntityName,
                       corr.LegalEntityBulstat,
                       userContext);
                    break;
                case "ForeignLegalEntity":
                    newCorr = this.CreateFLegalEntity(
                        corr.CorrespondentGroupId.Value,
                        corr.CorrespondentTypeId.Value,
                        true,
                        corr.FLegalEntityName,
                        corr.FLegalEntityCountryId,
                        corr.FLegalEntityRegisterName,
                        corr.FLegalEntityRegisterNumber,
                        corr.FLegalEntityOtherData,
                        userContext);
                    break;
                default:
                    newCorr = new Correspondent();
                    break;
            };

            newCorr.RegisterIndexId = corr.RegisterIndexId;
            newCorr.Email = corr.Email;
            newCorr.ContactDistrictId = corr.ContactDistrictId;
            newCorr.ContactMunicipalityId = corr.ContactMunicipalityId;
            newCorr.ContactSettlementId = corr.ContactSettlementId;
            newCorr.ContactPostCode = corr.ContactPostCode;
            newCorr.ContactAddress = corr.ContactAddress;
            newCorr.ContactPostOfficeBox = corr.ContactPostOfficeBox;
            newCorr.ContactPhone = corr.ContactPhone;
            newCorr.ContactFax = corr.ContactFax;
            newCorr.Alias = corr.Alias;
            newCorr.IsActive = corr.IsActive;

            foreach (var cc in corr.CorrespondentContacts.Where(e => !e.IsDeleted))
            {
                newCorr.CreateCorrespondentContact(cc.Name, cc.UIN, cc.Note, cc.IsActive, userContext);
            }

            this.unitOfWork.Save();

            return new CorrespondentDO(newCorr);
        }

        public List<Correspondent> GetCorrespondents(
            string displayName,
            string correspondentEmail,
            int limit,
            int offset)
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
