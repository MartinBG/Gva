using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class Correspondent
    {
        public Correspondent()
        {
            this.CorrespondentContacts = new List<CorrespondentContact>();
            this.DocCorrespondents = new List<DocCorrespondent>();
        }

        public int CorrespondentId { get; set; }

        public int CorrespondentGroupId { get; set; }

        public int? RegisterIndexId { get; set; }

        public string Email { get; set; }

        public string DisplayName { get; set; }

        public int CorrespondentTypeId { get; set; }

        public string BgCitizenFirstName { get; set; }

        public string BgCitizenLastName { get; set; }

        public string BgCitizenUIN { get; set; }

        public string ForeignerFirstName { get; set; }

        public string ForeignerLastName { get; set; }

        public int? ForeignerCountryId { get; set; }

        public string ForeignerSettlement { get; set; }

        public DateTime? ForeignerBirthDate { get; set; }

        public string LegalEntityName { get; set; }

        public string LegalEntityBulstat { get; set; }

        public string FLegalEntityName { get; set; }

        public int? FLegalEntityCountryId { get; set; }

        public string FLegalEntityRegisterName { get; set; }

        public string FLegalEntityRegisterNumber { get; set; }

        public string FLegalEntityOtherData { get; set; }

        public int? ContactDistrictId { get; set; }

        public int? ContactMunicipalityId { get; set; }

        public int? ContactSettlementId { get; set; }

        public string ContactPostCode { get; set; }

        public string ContactAddress { get; set; }

        public string ContactPostOfficeBox { get; set; }

        public string ContactPhone { get; set; }

        public string ContactFax { get; set; }

        public DateTime? ModifyDate { get; set; }

        public int? ModifyUserId { get; set; }

        public string Alias { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<CorrespondentContact> CorrespondentContacts { get; set; }

        public virtual CorrespondentGroup CorrespondentGroup { get; set; }

        public virtual CorrespondentType CorrespondentType { get; set; }

        public virtual Common.Api.Models.Country Country { get; set; }

        public virtual Common.Api.Models.Country Country1 { get; set; }

        public virtual Common.Api.Models.District District { get; set; }

        public virtual Common.Api.Models.Municipality Municipality { get; set; }

        public virtual RegisterIndex RegisterIndex { get; set; }

        public virtual Common.Api.Models.Settlement Settlement { get; set; }

        public virtual Common.Api.Models.User User { get; set; }

        public virtual ICollection<DocCorrespondent> DocCorrespondents { get; set; }
    }

    public class CorrespondentMap : EntityTypeConfiguration<Correspondent>
    {
        public CorrespondentMap()
        {
            // Primary Key
            this.HasKey(t => t.CorrespondentId);

            // Properties
            this.Property(t => t.Email)
                .HasMaxLength(200);

            this.Property(t => t.DisplayName)
                .HasMaxLength(400);

            this.Property(t => t.BgCitizenFirstName)
                .HasMaxLength(200);

            this.Property(t => t.BgCitizenLastName)
                .HasMaxLength(200);

            this.Property(t => t.BgCitizenUIN)
                .HasMaxLength(50);

            this.Property(t => t.ForeignerFirstName)
                .HasMaxLength(200);

            this.Property(t => t.ForeignerLastName)
                .HasMaxLength(200);

            this.Property(t => t.ForeignerSettlement)
                .HasMaxLength(50);

            this.Property(t => t.LegalEntityName)
                .HasMaxLength(200);

            this.Property(t => t.LegalEntityBulstat)
                .HasMaxLength(50);

            this.Property(t => t.FLegalEntityName)
                .HasMaxLength(200);

            this.Property(t => t.FLegalEntityRegisterName)
                .HasMaxLength(50);

            this.Property(t => t.FLegalEntityRegisterNumber)
                .HasMaxLength(50);

            this.Property(t => t.ContactPostCode)
                .HasMaxLength(20);

            this.Property(t => t.ContactPostOfficeBox)
                .HasMaxLength(100);

            this.Property(t => t.ContactPhone)
                .HasMaxLength(100);

            this.Property(t => t.ContactFax)
                .HasMaxLength(100);

            this.Property(t => t.Alias)
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Correspondents");
            this.Property(t => t.CorrespondentId).HasColumnName("CorrespondentId");
            this.Property(t => t.CorrespondentGroupId).HasColumnName("CorrespondentGroupId");
            this.Property(t => t.RegisterIndexId).HasColumnName("RegisterIndexId");
            this.Property(t => t.Email).HasColumnName("Email");
            this.Property(t => t.DisplayName).HasColumnName("DisplayName");
            this.Property(t => t.CorrespondentTypeId).HasColumnName("CorrespondentTypeId");
            this.Property(t => t.BgCitizenFirstName).HasColumnName("BgCitizenFirstName");
            this.Property(t => t.BgCitizenLastName).HasColumnName("BgCitizenLastName");
            this.Property(t => t.BgCitizenUIN).HasColumnName("BgCitizenUIN");
            this.Property(t => t.ForeignerFirstName).HasColumnName("ForeignerFirstName");
            this.Property(t => t.ForeignerLastName).HasColumnName("ForeignerLastName");
            this.Property(t => t.ForeignerCountryId).HasColumnName("ForeignerCountryId");
            this.Property(t => t.ForeignerSettlement).HasColumnName("ForeignerSettlement");
            this.Property(t => t.ForeignerBirthDate).HasColumnName("ForeignerBirthDate");
            this.Property(t => t.LegalEntityName).HasColumnName("LegalEntityName");
            this.Property(t => t.LegalEntityBulstat).HasColumnName("LegalEntityBulstat");
            this.Property(t => t.FLegalEntityName).HasColumnName("FLegalEntityName");
            this.Property(t => t.FLegalEntityCountryId).HasColumnName("FLegalEntityCountryId");
            this.Property(t => t.FLegalEntityRegisterName).HasColumnName("FLegalEntityRegisterName");
            this.Property(t => t.FLegalEntityRegisterNumber).HasColumnName("FLegalEntityRegisterNumber");
            this.Property(t => t.FLegalEntityOtherData).HasColumnName("FLegalEntityOtherData");
            this.Property(t => t.ContactDistrictId).HasColumnName("ContactDistrictId");
            this.Property(t => t.ContactMunicipalityId).HasColumnName("ContactMunicipalityId");
            this.Property(t => t.ContactSettlementId).HasColumnName("ContactSettlementId");
            this.Property(t => t.ContactPostCode).HasColumnName("ContactPostCode");
            this.Property(t => t.ContactAddress).HasColumnName("ContactAddress");
            this.Property(t => t.ContactPostOfficeBox).HasColumnName("ContactPostOfficeBox");
            this.Property(t => t.ContactPhone).HasColumnName("ContactPhone");
            this.Property(t => t.ContactFax).HasColumnName("ContactFax");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.ModifyUserId).HasColumnName("ModifyUserId");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.CorrespondentGroup)
                .WithMany(t => t.Correspondents)
                .HasForeignKey(d => d.CorrespondentGroupId);
            this.HasRequired(t => t.CorrespondentType)
                .WithMany(t => t.Correspondents)
                .HasForeignKey(d => d.CorrespondentTypeId);
            this.HasOptional(t => t.Country)
                .WithMany()
                .HasForeignKey(d => d.ForeignerCountryId);
            this.HasOptional(t => t.Country1)
                .WithMany()
                .HasForeignKey(d => d.FLegalEntityCountryId);
            this.HasOptional(t => t.District)
                .WithMany()
                .HasForeignKey(d => d.ContactDistrictId);
            this.HasOptional(t => t.Municipality)
                .WithMany()
                .HasForeignKey(d => d.ContactMunicipalityId);
            this.HasOptional(t => t.RegisterIndex)
                .WithMany()
                .HasForeignKey(d => d.RegisterIndexId);
            this.HasOptional(t => t.Settlement)
                .WithMany()
                .HasForeignKey(d => d.ContactSettlementId);
            this.HasOptional(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.ModifyUserId);
        }
    }
}
