using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class CorrespondentDO
    {
        public CorrespondentDO()
        {
            this.CorrespondentContacts = new List<CorrespondentContactDO>();
        }

        public CorrespondentDO(Correspondent c)
            : this()
        {
            if (c != null)
            {
                this.CorrespondentId = c.CorrespondentId;
                this.CorrespondentGroupId = c.CorrespondentGroupId;
                this.RegisterIndexId = c.RegisterIndexId;
                this.Email = c.Email;
                this.DisplayName = c.DisplayName;
                this.CorrespondentTypeId = c.CorrespondentTypeId;
                //bgcitizen
                this.BgCitizenFirstName = c.BgCitizenFirstName;
                this.BgCitizenLastName = c.BgCitizenLastName;
                this.BgCitizenUIN = c.BgCitizenUIN;
                //foreigner
                this.ForeignerFirstName = c.ForeignerFirstName;
                this.ForeignerCountryId = c.ForeignerCountryId;
                this.ForeignerLastName = c.ForeignerLastName;
                this.ForeignerSettlement = c.ForeignerSettlement;
                this.ForeignerBirthDate = c.ForeignerBirthDate;
                //legal entity
                this.LegalEntityName = c.LegalEntityName;
                this.LegalEntityBulstat = c.LegalEntityBulstat;
                //foreign legal entity
                this.FLegalEntityName = c.FLegalEntityName;
                this.FLegalEntityCountryId = c.FLegalEntityCountryId;
                this.FLegalEntityRegisterName = c.FLegalEntityRegisterName;
                this.FLegalEntityRegisterNumber = c.FLegalEntityRegisterNumber;
                this.FLegalEntityOtherData = c.FLegalEntityOtherData;

                //contact
                this.ContactDistrictId = c.ContactDistrictId;
                this.ContactMunicipalityId = c.ContactMunicipalityId;
                this.ContactSettlementId = c.ContactSettlementId;
                this.ContactPostCode = c.ContactPostCode;
                this.ContactAddress = c.ContactAddress;
                this.ContactPostOfficeBox = c.ContactPostOfficeBox;
                this.ContactPhone = c.ContactPhone;
                this.ContactFax = c.ContactFax;

                this.Alias = c.Alias;
                this.IsActive = c.IsActive;
                this.Version = c.Version;

                this.RegisterIndexCodeName = c.RegisterIndex != null ? string.Format("{0} {1}", c.RegisterIndex.Code, c.RegisterIndex.Name) : string.Empty;

                if (c.CorrespondentType != null)
                {
                    this.CorrespondentTypeAlias = c.CorrespondentType.Alias;
                    this.CorrespondentTypeName = c.CorrespondentType.Name;
                }
            }
        }

        public Nullable<int> CorrespondentId { get; set; }
        public Nullable<int> CorrespondentGroupId { get; set; }
        public Nullable<int> RegisterIndexId { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; private set; }
        public Nullable<int> CorrespondentTypeId { get; set; }
        public string BgCitizenFirstName { get; set; }
        public string BgCitizenLastName { get; set; }
        public string BgCitizenUIN { get; set; }
        public string ForeignerFirstName { get; set; }
        public string ForeignerLastName { get; set; }
        public Nullable<int> ForeignerCountryId { get; set; }
        public string ForeignerSettlement { get; set; }
        public Nullable<System.DateTime> ForeignerBirthDate { get; set; }
        public string LegalEntityName { get; set; }
        public string LegalEntityBulstat { get; set; }
        public string FLegalEntityName { get; set; }
        public Nullable<int> FLegalEntityCountryId { get; set; }
        public string FLegalEntityRegisterName { get; set; }
        public string FLegalEntityRegisterNumber { get; set; }
        public string FLegalEntityOtherData { get; set; }
        public Nullable<int> ContactDistrictId { get; set; }
        public Nullable<int> ContactMunicipalityId { get; set; }
        public Nullable<int> ContactSettlementId { get; set; }
        public string ContactPostCode { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPostOfficeBox { get; set; }
        public string ContactPhone { get; set; }
        public string ContactFax { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }

        //
        public string ErrorString { get; set; }

        public string RegisterIndexCodeName { get; set; }
        public string CorrespondentTypeAlias { get; set; }
        public string CorrespondentTypeName { get; set; }

        public List<CorrespondentContactDO> CorrespondentContacts { get; set; }

        #region Visibility Flags

        public bool IsVisibleBgCitizen { get; set; }
        public bool IsVisibleForeigner { get; set; }
        public bool IsVisibleLegalEntity { get; set; }
        public bool IsVisibleForeignLegalEntity { get; set; }

        public void SetupFlags()
        {
            switch (this.CorrespondentTypeAlias)
            {
                case "BulgarianCitizen":
                    this.IsVisibleBgCitizen = true;
                    break;
                case "Foreigner":
                    this.IsVisibleForeigner = true;
                    break;
                case "LegalEntity":
                    this.IsVisibleLegalEntity = true;
                    break;
                case "ForeignLegalEntity":
                    this.IsVisibleForeignLegalEntity = true;
                    break;
            };
        }

        #endregion
    }
}
