using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class DeregCert : IDataGenerator
    {
        private ILotRepository lotRepository;
        private IPersonRepository personRepository;
        private IOrganizationRepository organizationRepository;
        private INomRepository nomRepository;

        public DeregCert(
            ILotRepository lotRepository,
            IPersonRepository personRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.personRepository = personRepository;
            this.organizationRepository = organizationRepository;
            this.nomRepository = nomRepository;
        }

        public string GeneratorCode
        {
            get
            {
                return "deregCert";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Удостоверение за дерегистрация";
            }
        }

        public object GetData(int lotId, string path)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            AircraftDataDO aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
            AircraftCertRegistrationFMDO registration = lot.Index.GetPart<AircraftCertRegistrationFMDO>(path).Content;
            IEnumerable<AircraftDocumentDebtFMDO> debts = lot.Index.GetParts<AircraftDocumentDebtFMDO>("aircraftDocumentDebtsFM")
                .Select(d => d.Content)
                .OrderBy(d => d.DocumentDate);

            List<object> formatedDebts = new List<object>();
            List<object> formatedDebtsAlt = new List<object>();
            
            if(debts.Count() > 0)
            {
                foreach(var debtGroup in debts.GroupBy(d => d.AircraftApplicant.Name).ToList())
                {
                    string debtsTitle = string.Format("Нашите записи показват неотменени тежести в полза на {0}:", debtGroup.First().AircraftApplicant.Name);
                    string debtsTitleAlt = string.Format("Our records show unreleased liens (mortgages registrations) in favour of {0}:", debtGroup.First().AircraftApplicant.NameAlt);
                    List<object> data = new List<object>();
                    List<object> dataAlt = new List<object>();
                    int debtOrder = 1;
                    foreach (var debt in debtGroup)
                    {
                        string regDate = debt.RegDate.HasValue ? debt.RegDate.Value.ToString("dd.MM.yyy") : string.Empty;
                        string documentDate = debt.DocumentDate.HasValue ? debt.DocumentDate.Value.ToString("dd.MM.yyy") : string.Empty;
                        data.Add(string.Format("{0}. {1} вписан на {2}, изх ....... , ГВА вх. {3}/{4}, {5}", debtOrder, debt.AircraftDebtType.Name, regDate, debt.DocumentNumber, documentDate, debt.Notes));
                        dataAlt.Add(string.Format("{0}. {1} entered on {2}, letter #......., CAA#{3}/{4}, {5}", debtOrder, debt.AircraftDebtType.NameAlt, regDate, debt.DocumentNumber, documentDate, debt.Notes));
                        debtOrder++;
                    }

                    formatedDebts.Add(new
                    {
                        TITLE = debtsTitle,
                        DATA = data.Select(d => new { INFO = d })
                    });

                    formatedDebtsAlt.Add(new
                    {
                        TITLE = debtsTitleAlt,
                        DATA = dataAlt.Select(d => new { INFO = d })
                    });
                }
            }

            Tuple<string, string, string, string> ownerData = null;
            Tuple<string, string, string, string> operatorData = null;

            if (registration.OwnerOrganization != null)
            {
                ownerData = this.GetOrganizationNamesAndAddress(registration.OwnerOrganization.NomValueId);
            }
            else if (registration.OwnerPerson != null)
            {
                ownerData = this.GetPersonNamesAndAddress(registration.OwnerPerson.NomValueId);
            }

            if (registration.OperOrganization != null)
            {
                operatorData = this.GetOrganizationNamesAndAddress(registration.OperOrganization.NomValueId);
            }
            else if (registration.OperPerson != null)
            {
                operatorData = this.GetPersonNamesAndAddress(registration.OperPerson.NomValueId);
            }

            var json = new
            {
                root = new
                {
                    DOCUMENT_NUMBER = registration.CertNumber,
                    PRODUCER_ALT = aircraftData.AircraftProducer != null ? aircraftData.AircraftProducer.NameAlt : null,
                    AIRCRAFT_TYPE = aircraftData.ModelAlt,
                    REG_MARK = registration != null ? registration.RegMark : null,
                    REG_NUMBER = registration != null ? registration.CertNumber : null,
                    MSN = aircraftData.ManSN,
                    OWNER_ADDRESS = ownerData != null ? ownerData.Item3 : null,
                    OWNER_ADDRESS_ALT = ownerData != null ? ownerData.Item4 : null,
                    OWNER_NAME = ownerData != null ? ownerData.Item1 : null,
                    OWNER_NAME_ALT = ownerData != null ? ownerData.Item2 : null,
                    OPERATOR_NAME = operatorData != null ? operatorData.Item1 : null,
                    OPERATOR_NAME_ALT = operatorData != null ? operatorData.Item2 : null,
                    LAST_REG_DATE = string.Format("{0:dd.MM.yyyy}", registration.CertDate),
                    REMOVAL_DATE = registration.Removal.Date,
                    REMOVAL_REASON = registration.Removal.Reason != null ? registration.Removal.Reason.Name : null,
                    REMOVAL_REASON_ALT = registration.Removal.Reason != null ? registration.Removal.Reason.NameAlt : null,
                    NOTES = registration.Removal.Notes,
                    NOTES_ALT = registration.Removal.NotesAlt,
                    NO_DEBTS = formatedDebts.Count() == 0 ? "Нашите записи не показват неотменени тежести върху ВС-то." : null,
                    NO_DEBTS_ALT = formatedDebts.Count() == 0 ? "Our records show no unreleased liens (mortgages registrations) against aircraft" : null,
                    DEBTS = formatedDebts,
                    DEBTS_ALT = formatedDebtsAlt
                }
            };

            return json;
        }

        private Tuple<string, string, string, string> GetPersonNamesAndAddress(int personId)
        {
            var person = this.personRepository.GetPerson(personId);
            int validTrueId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;
            var personAddress = this.lotRepository.GetLotIndex(personId).Index
                .GetParts<PersonAddressDO>("personAddresses")
                    .Where(a => a.Content.ValidId == validTrueId)
                    .FirstOrDefault();
            string address = personAddress != null ? personAddress.Content.Address : null;
            string addressAlt = personAddress != null ? personAddress.Content.AddressAlt : null;

            return new Tuple<string, string, string, string>(person.Names, person.NamesAlt, address, addressAlt);
        }

        private Tuple<string, string, string, string> GetOrganizationNamesAndAddress(int organizationId)
        {
            var organization = this.organizationRepository.GetOrganization(organizationId);
            var organizationAddress = this.lotRepository.GetLotIndex(organizationId).Index
                .GetParts<OrganizationAddressDO>("organizationAddresses")
                    .Where(a => a.Content.Valid.Code == "Y")
                    .FirstOrDefault();
            string address = organizationAddress != null ? organizationAddress.Content.Address : null;
            string addressAlt = organizationAddress != null ? organizationAddress.Content.AddressAlt : null;

            return new Tuple<string, string, string, string>(organization.Name, organization.NameAlt, address, addressAlt);
        }
    }
}
