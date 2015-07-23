using System;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
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

            Tuple<string, string, string, string> ownerData = null;
            Tuple<string, string, string, string> operatorData = null;

            if(registration.OwnerOrganization != null)
            {
                ownerData = this.GetOrganizationNamesAndAddress(registration.OwnerOrganization.NomValueId);
            }
            else if(registration.OwnerPerson != null)
            {
               ownerData = this.GetPersonNamesAndAddress(registration.OwnerPerson.NomValueId);
            }
           
            if(registration.OperOrganization != null)
            {
                operatorData = this.GetOrganizationNamesAndAddress(registration.OperOrganization.NomValueId);
            }
            else if(registration.OperPerson != null)
            {
                operatorData = this.GetPersonNamesAndAddress(registration.OperPerson.NomValueId);
            }

            var json = new
            {
                root = new
                {
                    DOCUMENT_NUMBER = registration.CertNumber, 
                    PRODUCER_ALT = aircraftData.AircraftProducer != null ? aircraftData.AircraftProducer.NameAlt : null,
                    REG_MARK = registration != null ? registration.RegMark : null,
                    MSN = aircraftData.ManSN,
                    OWNER_ADDRESS = ownerData != null ? ownerData.Item3 : null,
                    OWNER_ADDRESS_ALT = ownerData != null ? ownerData.Item4 : null,
                    OWNER_NAME = ownerData != null ? ownerData.Item1 : null,
                    OWNER_NAME_ALT = ownerData != null ? ownerData.Item2 : null,
                    OPERATOR_NAME = operatorData != null ? operatorData.Item1 : null,
                    OPERATOR_NAME_ALT = operatorData != null ? operatorData.Item2 : null,
                    LAST_REG_DATE = string.Format("{0:dd.MM.yyyy}", registration.CertDate),
                    REMOVAL_DATE = registration.Removal.Date,
                }
            };

            return json;
        }

        private Tuple<string, string, string, string> GetPersonNamesAndAddress(int personId)
        {
            var person = this.personRepository.GetPerson(personId);
            var personAddress = this.lotRepository.GetLotIndex(personId).Index
                .GetParts<PersonAddressDO>("personAddresses")
                    .Where(a => this.nomRepository.GetNomValue("boolean", a.Content.ValidId.Value).Code == "Y")
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
