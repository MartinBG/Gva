using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Regs.Api.Models;

namespace Gva.Api.Models
{
    public partial class GvaViewPersonInspector
    {
        public int? LotId { get; set; }

        public string ExaminerCode { get; set; }

        public string CaaName { get; set; }

        public string StampNum { get; set; }

        public bool IsValid { get; set; }

        public virtual Lot Lot { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonInspectorMap : EntityTypeConfiguration<GvaViewPersonInspector>
    {
        public GvaViewPersonInspectorMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ExaminerCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.CaaName)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.StampNum)
                .IsOptional()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonInspectors");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.ExaminerCode).HasColumnName("ExaminerCode");
            this.Property(t => t.CaaName).HasColumnName("CaaName");
            this.Property(t => t.StampNum).HasColumnName("StampNum");
            this.Property(t => t.IsValid).HasColumnName("IsValid");

            // Relationships
            this.HasRequired(t => t.Lot)
                .WithOptional();

            // Hack mapping to workaround joins
            this.HasRequired(t => t.Person)
                .WithOptional(t => t.Inspector);
        }
    }
}
