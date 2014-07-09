using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonLicence
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int LicenceTypeId { get; set; }

        public virtual NomValue LicenceType { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonLicenceMap : EntityTypeConfiguration<GvaViewPersonLicence>
    {
        public GvaViewPersonLicenceMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            this.Property(t => t.PartIndex)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonLicences");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.LicenceTypeId).HasColumnName("LicenceTypeId");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithMany(t => t.Licences)
                .HasForeignKey(d => d.LotId);
            this.HasRequired(t => t.LicenceType)
                .WithMany()
                .HasForeignKey(t => t.LicenceTypeId);
        }
    }
}
