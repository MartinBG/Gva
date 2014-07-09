using System.Data.Entity.ModelConfiguration;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Models.Views.Organization
{
    public partial class GvaViewOrganizationExaminer
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int PersonId { get; set; }

        public string ExaminerCode { get; set; }

        public string StampNum { get; set; }

        public bool PermitedAW { get; set; }

        public bool PermitedCheck { get; set; }

        public bool Valid { get; set; }

        public virtual GvaViewOrganization Organization { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewOrganizationExaminerMap : EntityTypeConfiguration<GvaViewOrganizationExaminer>
    {
        public GvaViewOrganizationExaminerMap()
        {
            // Primary Key
            this.HasKey(t => new { t.LotId, t.PartIndex });

            // Properties
            this.Property(t => t.ExaminerCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StampNum)
                .IsOptional()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewOrganizationExaminers");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.PartIndex).HasColumnName("PartIndex");
            this.Property(t => t.PersonId).HasColumnName("PersonId");
            this.Property(t => t.ExaminerCode).HasColumnName("ExaminerCode");
            this.Property(t => t.StampNum).HasColumnName("StampNum");
            this.Property(t => t.PermitedAW).HasColumnName("PermitedAW");
            this.Property(t => t.PermitedCheck).HasColumnName("PermitedCheck");
            this.Property(t => t.Valid).HasColumnName("Valid");

            // Relationships
            this.HasRequired(t => t.Organization)
                .WithMany(t => t.Examiners)
                .HasForeignKey(t => t.LotId);

            this.HasRequired(t => t.Person)
                .WithMany(t => t.OrganizationExaminers)
                .HasForeignKey(t => t.PersonId);
        }
    }
}
