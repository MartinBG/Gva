﻿using System;
using System.Xml;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.PersonRepository;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Xml.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace Gva.Api.Controllers.Persons
{
    public class PersonExportXmlController : ApiController
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public PersonExportXmlController(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
        }

        [Route("api/exportXml/personsData")]
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

        private void AppendPersonData(int id, XElement row)
        {
            PersonDataDO person = this.lotRepository.GetLotIndex(id).Index.GetPart<PersonDataDO>("personData").Content;
            var personAddress = this.lotRepository.GetLotIndex(id).Index.GetParts<PersonAddressDO>("personAddresses")
                    .Where(a => a.Content.Valid.Code == "Y")
                    .FirstOrDefault();
            row.Add(new XElement("NAME", person.FirstName));
            row.Add(new XElement("SURNAME", person.MiddleName));
            row.Add(new XElement("FAMILY", person.LastName));
            row.Add(new XElement("LIN", person.Lin.ToString()));
            row.Add(new XElement("LIN", person.Lin.ToString()));
            string settlement = personAddress != null? personAddress.Content.Settlement.Name : null;
            row.Add(new XElement("ADDR_EKNM_TOWN", settlement));

            string settlementType = personAddress != null && personAddress.Content.Settlement.Name.Contains(".") ? personAddress.Content.Settlement.Name.Split('.')[0] : null;
            row.Add(new XElement("ADDR_EKNM_TYPE", settlementType));

            string addrText = personAddress != null ? personAddress.Content.Address : null;
            row.Add(new XElement("ADDR_TEXT", addrText));

            row.Add(new XElement("E_MAIL", person.Email));
            row.Add(new XElement("PHONE_OFFICE", person.Phone1));
            row.Add(new XElement("PHONE_OTHER", person.Phone2));
            row.Add(new XElement("BIRTH_EKNM", person.PlaceOfBirth.Code));
            row.Add(new XElement("BIRTH_EKNM_TYPE", person.PlaceOfBirth.Name.Contains(".") ? person.PlaceOfBirth.Name.Split('.')[0] : null));
            row.Add(new XElement("BIRTH_EKNM_COUNTRY", person.Country.Name));
            row.Add(new XElement("BIRTH_EKNM_TOWN", person.PlaceOfBirth.Name));
            row.Add(new XElement("CITIZEN_COUNTRY_ID", person.Country.Code));
            row.Add(new XElement("CITIZEN_COUNTRY_NAME", person.Country.Name));
            row.Add(new XElement("DATE_OF_BIRTH", person.DateOfBirth.Value.ToShortDateString()));
            row.Add(new XElement("SEX_ID", person.Sex.Code));

            NomValue BG = this.nomRepository.GetNomValue("countries", "BG");
            row.Add(new XElement("FOREIGNER", person.Country.Code == BG.Code ? "N" : "Y"));

        }
    }
}