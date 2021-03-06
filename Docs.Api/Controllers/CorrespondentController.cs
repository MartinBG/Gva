﻿using Common.Api.Models;
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

            try
            {
                this.userContext = this.Request.GetUserContext();
            }
            catch { }
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
        public IHttpActionResult GetCorrespondents(
            int limit = 10,
            int offset = 0,
            string displayName = null,
            string correspondentEmail = null
            )
        {
            //? hot fix: load fist 1000 corrs, so the paging with datatable will work
            limit = 1000;
            offset = 0;

            var returnValue =
                this.correspondentRepository.GetCorrespondents(
                    displayName,
                    correspondentEmail,
                    limit,
                    offset)
                .Select(e => new CorrespondentDO(e))
                .ToList();

            return Ok(new
            {
                correspondents = returnValue,
                correspondentCount = returnValue.Count()
            });
        }

        /// <summary>
        /// Преглед на кореспондент
        /// </summary>
        /// <param name="id">Идентификатор на кореспондент</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCorrespondent(int id)
        {
            Correspondent correspondent = this.correspondentRepository.GetCorrespondent(id);

            if (correspondent == null)
            {
                return NotFound();
            }

            CorrespondentDO returnValue = new CorrespondentDO(correspondent);

            foreach (CorrespondentContact cc in correspondent.CorrespondentContacts.ToList())
            {
                returnValue.CorrespondentContacts.Add(new CorrespondentContactDO(cc));
            }

            returnValue.SetupFlags();

            return Ok(returnValue);
        }

        /// <summary>
        /// Генерира нов кореспондент
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetNewCorrespondent()
        {
            return Ok(this.correspondentRepository.GetNewCorrespondent());
        }

        /// <summary>
        /// Запис на нов кореспондент в базата данни
        /// </summary>
        /// <param name="corr">Данни на кореспондента</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult CreateCorrespondent(CorrespondentDO corr)
        {
            var result = this.correspondentRepository.CreateCorrespondent(corr, this.userContext);

            return Ok(new
                {
                    err = "",
                    correspondentId = result.CorrespondentId,
                    obj = result
                });
        }

        /// <summary>
        /// Редакция на кореспондент
        /// </summary>
        /// <param name="id">Идентификатор на кореспондент</param>
        /// <param name="corr">Нови данни на кореспондент</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateCorrespondent(int id, CorrespondentDO corr)
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

            return Ok(new
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
        public IHttpActionResult DeleteCorrespondent(int id, string corrVersion)
        {
            this.correspondentRepository.DeteleCorrespondent(id, Helper.StringToVersion(corrVersion));
            this.unitOfWork.Save();

            return Ok();
        }
    }
}
