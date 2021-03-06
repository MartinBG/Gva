﻿using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Xml.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Newtonsoft.Json;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/exportXml")]
    public class PersonExportXmlController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IApplicationRepository applicationRepository;

        public PersonExportXmlController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IApplicationRepository applicationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.applicationRepository = applicationRepository;
        }

        [Route("personsData")]
        [HttpGet]
        public HttpResponseMessage ExportPersonsData([FromUri]string personIds)
        {
            XElement rowset = new XElement("ROWSET");
            XDocument xmlDoc = new XDocument(
                 new XDeclaration("1.0", "utf-8", null),
                 rowset);

            foreach (int id in JsonConvert.DeserializeObject<List<int>>(personIds))
            {
                XElement row = new XElement("ROW");
                this.AppendPersonData(id, row);
                rowset.Add(row);
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(string.Concat(xmlDoc));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Export_FL.xml"
                };

            return result;
        }

        [Route("examsData")]
        [HttpGet]
        public HttpResponseMessage ExportExamsData([FromUri]string examsIds)
        {
            XElement rowset = new XElement("ROWSET");
            XDocument xmlDoc = new XDocument(
                 new XDeclaration("1.0", "utf-8", null),
                 rowset);

            foreach (int examId in JsonConvert.DeserializeObject<List<int>>(examsIds))
            {
                GvaViewPersonApplicationExam exam = this.unitOfWork.DbContext.Set<GvaViewPersonApplicationExam>()
                    .Where(a => a.AppExamId == examId)
                    .Single();

                XElement row = new XElement("ROW");
                this.AppendExamData(row, exam);
                rowset.Add(row);
            }

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(string.Concat(xmlDoc));
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/xml");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = "Export_Exam.xml"
                };

            return result;
        }


        private void AppendExamData(XElement row, GvaViewPersonApplicationExam exam)
        {
            PersonDataDO person = this.lotRepository.GetLotIndex(exam.LotId).Index.GetPart<PersonDataDO>("personData").Content;
            GvaViewApplication application = this.applicationRepository.GetApplicationByPartId(exam.AppPartId);

            row.Add(new XElement("DOC_NO", application.DocumentNumber));
            row.Add(new XElement("DOC_DATE", application.DocumentDate.HasValue ? application.DocumentDate.Value.ToString("dd/MM/yyyy") : null));
            row.Add(new XElement("EGN", person.Uin));
            row.Add(new XElement("LIN", person.Lin));
            row.Add(new XElement("CERT_CAMP_CODE", exam.CertCampCode));
            row.Add(new XElement("TEST_CODE", exam.ExamCode));
        }

        private void AppendPersonData(int id, XElement row)
        {
            PersonDataDO person = this.lotRepository.GetLotIndex(id).Index.GetPart<PersonDataDO>("personData").Content;
            int trueValidId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;
            var personAddress = this.lotRepository.GetLotIndex(id).Index.GetParts<PersonAddressDO>("personAddresses")
                    .Where(a => a.Content.ValidId == trueValidId)
                    .FirstOrDefault();

            row.Add(new XElement("NAME", person.FirstName));
            row.Add(new XElement("SURNAME", person.MiddleName));
            row.Add(new XElement("FAMILY", person.LastName));
            row.Add(new XElement("LIN", person.Lin.ToString()));
            row.Add(new XElement("LIN", person.Lin.ToString()));

            NomValue settlement = null;
            if (personAddress.Content.SettlementId.HasValue)
            {
                settlement = this.nomRepository.GetNomValue("cities", personAddress.Content.SettlementId.Value);
            }

            row.Add(new XElement("ADDR_EKNM_TOWN", settlement));

            string settlementType = personAddress != null && settlement != null && settlement.Name.Contains(".") ? settlement.Name.Split('.')[0] : null;
            row.Add(new XElement("ADDR_EKNM_TYPE", settlementType));

            string addrText = personAddress != null ? personAddress.Content.Address : null;
            row.Add(new XElement("ADDR_TEXT", addrText));

            NomValue country = person.CountryId.HasValue ? this.nomRepository.GetNomValue("countries", person.CountryId.Value) : null;
            NomValue placeOfBirth = person.PlaceOfBirthId.HasValue ? this.nomRepository.GetNomValue("cities", person.PlaceOfBirthId.Value) : null;
            NomValue gender = person.SexId.HasValue ? this.nomRepository.GetNomValue("gender", person.SexId.Value) : null;

            row.Add(new XElement("E_MAIL", person.Email));
            row.Add(new XElement("PHONE_OFFICE", person.Phone1));
            row.Add(new XElement("PHONE_OTHER", person.Phone2));
            row.Add(new XElement("BIRTH_EKNM", placeOfBirth != null ? placeOfBirth.Code : null));
            row.Add(new XElement("BIRTH_EKNM_TYPE", placeOfBirth != null && placeOfBirth.Name.Contains(".") ? placeOfBirth.Name.Split('.')[0] : null));
            row.Add(new XElement("BIRTH_EKNM_COUNTRY", country != null ? country.Name : null));
            row.Add(new XElement("BIRTH_EKNM_TOWN", placeOfBirth != null ? placeOfBirth.Name : null));
            row.Add(new XElement("CITIZEN_COUNTRY_ID", country != null ? country.Code : null));
            row.Add(new XElement("CITIZEN_COUNTRY_NAME", country != null ? country.Name : null));
            row.Add(new XElement("DATE_OF_BIRTH", person.DateOfBirth.Value.ToString("dd/MM/yyyy")));
            row.Add(new XElement("SEX_ID", gender != null ? gender.Code : null));

            NomValue BG = this.nomRepository.GetNomValue("countries", "BG");
            row.Add(new XElement("FOREIGNER", person.CountryId == BG.NomValueId ? "N" : "Y"));

        }
    }
}