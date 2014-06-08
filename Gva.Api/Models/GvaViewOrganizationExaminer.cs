using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewOrganizationExaminer
    {
        public int PartId { get; set; }

        public int? LotId { get; set; }

        public int? PersonLotId { get; set; }

        public string ExaminerCode { get; set; }

        public string StampNum { get; set; }

        public bool PermitedAW { get; set; }

        public bool PermitedCheck { get; set; }

        public bool IsValid { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual Part Part { get; set; }

        public virtual GvaViewOrganization Organization { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewOrganizationExaminerMap : EntityTypeConfiguration<GvaViewOrganizationExaminer>
    {
        public GvaViewOrganizationExaminerMap()
        {
            // Primary Key
            this.HasKey(t => t.PartId);

            // Properties
            this.Property(t => t.PartId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ExaminerCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StampNum)
                .IsOptional()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewOrganizationExaminers");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.PersonLotId).HasColumnName("PersonLotId");
            this.Property(t => t.ExaminerCode).HasColumnName("ExaminerCode");
            this.Property(t => t.StampNum).HasColumnName("StampNum");
            this.Property(t => t.PermitedAW).HasColumnName("PermitedAW");
            this.Property(t => t.PermitedCheck).HasColumnName("PermitedCheck");
            this.Property(t => t.IsValid).HasColumnName("IsValid");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithMany()
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Part)
                .WithOptional();

            this.HasRequired(t => t.Person)
                .WithMany(t => t.OrganizationExaminers)
                .HasForeignKey(t => t.PersonLotId);

            // Hack mapping to workaround joins
            this.HasRequired(t => t.Organization)
                .WithMany(t => t.Examiners)
                .HasForeignKey(t => t.LotId);
        }
    }
}
