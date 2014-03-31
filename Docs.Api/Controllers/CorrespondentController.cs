using Common.Api.Models;
using Docs.Api.DataObjects;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Extensions;

namespace Docs.Api.Controllers
{
    [Authorize]
    public class CorrespondentController : ApiController
    {
        private Common.Data.IUnitOfWork unitOfWork;
        private Docs.Api.Repositories.CorrespondentRepository.ICorrespondentRepository correspondentRepository;
        private UserContext userContext;

        public CorrespondentController(Common.Data.IUnitOfWork unitOfWork,
            Docs.Api.Repositories.CorrespondentRepository.ICorrespondentRepository correspondentRepository)
        {
            this.unitOfWork = unitOfWork;
            this.correspondentRepository = correspondentRepository;
        }

        protected override void Initialize(System.Web.Http.Controllers.HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);

            this.userContext = this.Request.GetUserContext();
        }

        /// <summary>
        /// Търсене на кореспондент
        /// </summary>
        /// <param name="correspondentUin">Филтър за търсене на кореспондент по ключова дума</param>
        /// <param name="correspondentEmail">>Филтър за търсене на кореспондент по имейл</param>
        /// <param name="limit">Брой резултати на страница</param>
        /// <param name="offset">Параметър за страницирането</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCorrespondents(
            int limit = 10,
            int offset = 0,
            string displayName = null,
            string correspondentEmail = null
            )
        {
            int totalCount = 0;

            var returnValue =
                this.correspondentRepository.GetCorrespondents(
                    displayName,
                    correspondentEmail,
                    limit,
                    offset,
                    out totalCount)
                .Select(e => new CorrespondentDO(e))
                .ToList();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, new
            {
                correspondents = returnValue,
                correspondentCount = totalCount
            });
        }

        /// <summary>
        /// Преглед на кореспондент
        /// </summary>
        /// <param name="id">Идентификатор на кореспондент</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetCorrespondent(int id)
        {
            Correspondent correspondent = this.correspondentRepository.GetCorrespondent(id);

            if (correspondent == null)
            {
                return ControllerContext.Request.CreateResponse(HttpStatusCode.NoContent);
            }

            CorrespondentDO returnValue = new CorrespondentDO(correspondent);

            foreach (CorrespondentContact cc in correspondent.CorrespondentContacts.ToList())
            {
                returnValue.CorrespondentContacts.Add(new CorrespondentContactDO(cc));
            }

            returnValue.SetupFlags();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        /// <summary>
        /// Генерира нов кореспондент
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetNewCorrespondent()
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

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK, returnValue);
        }

        /// <summary>
        /// Запис на нов кореспондент в базата данни
        /// </summary>
        /// <param name="corr">Данни на кореспондента</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage CreateCorrespondent(CorrespondentDO corr)
        {
            Correspondent newCorr;

            CorrespondentType correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                .SingleOrDefault(e => e.CorrespondentTypeId == corr.CorrespondentTypeId);

            switch (correspondentType.Alias)
            {
                case "BulgarianCitizen":
                    newCorr = this.correspondentRepository.CreateBgCitizen(
                        corr.CorrespondentGroupId.Value,
                        corr.CorrespondentTypeId.Value,
                        true,
                        corr.BgCitizenFirstName,
                        corr.BgCitizenLastName,
                        corr.BgCitizenUIN,
                        this.userContext);
                    break;
                case "Foreigner":
                    newCorr = this.correspondentRepository.CreateForeigner(
                        corr.CorrespondentGroupId.Value,
                        corr.CorrespondentTypeId.Value,
                        true,
                        corr.ForeignerFirstName,
                        corr.ForeignerLastName,
                        corr.ForeignerCountryId,
                        corr.ForeignerSettlement,
                        corr.ForeignerBirthDate,
                        this.userContext);
                    break;
                case "LegalEntity":
                    newCorr = this.correspondentRepository.CreateLegalEntity(
                       corr.CorrespondentGroupId.Value,
                       corr.CorrespondentTypeId.Value,
                       true,
                       corr.LegalEntityName,
                       corr.LegalEntityBulstat,
                       this.userContext);
                    break;
                case "ForeignLegalEntity":
                    newCorr = this.correspondentRepository.CreateFLegalEntity(
                        corr.CorrespondentGroupId.Value,
                        corr.CorrespondentTypeId.Value,
                        true,
                        corr.FLegalEntityName,
                        corr.FLegalEntityCountryId,
                        corr.FLegalEntityRegisterName,
                        corr.FLegalEntityRegisterNumber,
                        corr.FLegalEntityOtherData,
                        this.userContext);
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
                newCorr.CreateCorrespondentContact(cc.Name, cc.UIN, cc.Note, cc.IsActive, this.userContext);
            }

            this.unitOfWork.Save();

            CorrespondentDO returnValue = new CorrespondentDO(newCorr);

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    err = "",
                    correspondentId = returnValue.CorrespondentId,
                    obj = returnValue
                });
        }

        /// <summary>
        /// Редакция на кореспондент
        /// </summary>
        /// <param name="id">Идентификатор на кореспондент</param>
        /// <param name="corr">Нови данни на кореспондент</param>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage UpdateCorrespondent(int id, CorrespondentDO corr)
        {
            var oldCorr = this.correspondentRepository.GetCorrespondent(id);

            oldCorr.EnsureForProperVersion(corr.Version);

            oldCorr.BgCitizenFirstName = null;
            oldCorr.BgCitizenLastName = null;
            oldCorr.BgCitizenUIN = null;

            oldCorr.ForeignerFirstName = null;
            oldCorr.ForeignerLastName = null;
            oldCorr.ForeignerCountry = null;
            oldCorr.ForeignerCountryId = null;
            oldCorr.ForeignerSettlement = null;
            oldCorr.ForeignerBirthDate = null;

            oldCorr.LegalEntityName = null;
            oldCorr.LegalEntityBulstat = null;

            oldCorr.FLegalEntityName = null;
            oldCorr.FLegalEntityCountry = null;
            oldCorr.FLegalEntityCountryId = null;
            oldCorr.FLegalEntityRegisterName = null;
            oldCorr.FLegalEntityRegisterNumber = null;
            oldCorr.FLegalEntityOtherData = null;

            CorrespondentType correspondentType = this.unitOfWork.DbContext.Set<CorrespondentType>()
                .SingleOrDefault(e => e.CorrespondentTypeId == corr.CorrespondentTypeId);

            switch (correspondentType.Alias)
            {
                case "BulgarianCitizen":
                    oldCorr.BgCitizenFirstName = corr.BgCitizenFirstName;
                    oldCorr.BgCitizenLastName = corr.BgCitizenLastName;
                    oldCorr.BgCitizenUIN = corr.BgCitizenUIN;
                    break;
                case "Foreigner":
                    oldCorr.ForeignerFirstName = corr.ForeignerFirstName;
                    oldCorr.ForeignerLastName = corr.ForeignerLastName;
                    oldCorr.ForeignerCountryId = corr.ForeignerCountryId;
                    oldCorr.ForeignerSettlement = corr.ForeignerSettlement;
                    oldCorr.ForeignerBirthDate = corr.ForeignerBirthDate;
                    break;
                case "LegalEntity":
                    oldCorr.LegalEntityName = corr.LegalEntityName;
                    oldCorr.LegalEntityBulstat = corr.LegalEntityBulstat;
                    break;
                case "ForeignLegalEntity":
                    oldCorr.FLegalEntityName = corr.FLegalEntityName;
                    oldCorr.FLegalEntityCountryId = corr.FLegalEntityCountryId;
                    oldCorr.FLegalEntityRegisterName = corr.FLegalEntityRegisterName;
                    oldCorr.FLegalEntityRegisterNumber = corr.FLegalEntityRegisterNumber;
                    oldCorr.FLegalEntityOtherData = corr.FLegalEntityOtherData;
                    break;
            };

            oldCorr.CorrespondentGroupId = corr.CorrespondentGroupId.Value;
            oldCorr.RegisterIndexId = corr.RegisterIndexId;
            oldCorr.Email = corr.Email;
            oldCorr.CorrespondentTypeId = corr.CorrespondentTypeId.Value;
            oldCorr.ContactDistrictId = corr.ContactDistrictId;
            oldCorr.ContactMunicipalityId = corr.ContactMunicipalityId;
            oldCorr.ContactSettlementId = corr.ContactSettlementId;
            oldCorr.ContactPostCode = corr.ContactPostCode;
            oldCorr.ContactAddress = corr.ContactAddress;
            oldCorr.ContactPostOfficeBox = corr.ContactPostOfficeBox;
            oldCorr.ContactPhone = corr.ContactPhone;
            oldCorr.ContactFax = corr.ContactFax;
            oldCorr.Alias = corr.Alias;
            oldCorr.IsActive = corr.IsActive;

            oldCorr.ModifyDate = DateTime.Now;
            oldCorr.ModifyUserId = this.userContext.UserId;

            foreach (CorrespondentContactDO cc in corr.CorrespondentContacts.Where(e => !e.IsNew && e.IsDeleted && e.CorrespondentContactId.HasValue))
            {
                oldCorr.DeleteCorrespondentContact(cc.CorrespondentContactId.Value, this.userContext);
            }

            foreach (var cc in corr.CorrespondentContacts.Where(e => e.IsDirty && !e.IsNew && !e.IsDeleted && e.CorrespondentContactId.HasValue))
            {
                oldCorr.UpdateCorrespondentContact(
                    cc.CorrespondentContactId.Value,
                    cc.Name,
                    cc.UIN,
                    cc.Note,
                    cc.IsActive,
                    this.userContext);
            }

            foreach (var cc in corr.CorrespondentContacts.Where(e => e.IsNew && !e.IsDeleted))
            {
                oldCorr.CreateCorrespondentContact(cc.Name, cc.UIN, cc.Note, cc.IsActive, this.userContext);
            }

            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK,
                new
                {
                    err = "",
                    correspondentId = oldCorr.CorrespondentId
                });
        }

        /// <summary>
        /// Изтриване на кореспондент
        /// </summary>
        /// <param name="id">Идентификатор на кореспондент</param>
        /// <returns></returns>
        [HttpDelete]
        public HttpResponseMessage DeleteCorrespondent(int id, string corrVersion)
        {
            this.correspondentRepository.DeteleCorrespondent(id, Helper.StringToVersion(corrVersion));
            this.unitOfWork.Save();

            return ControllerContext.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
