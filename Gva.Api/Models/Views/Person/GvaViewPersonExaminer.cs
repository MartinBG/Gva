using Regs.Api.LotEvents;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonExaminer : IProjectionView
    {
        public int LotId { get; set; }

        public string ExaminerCode { get; set; }

        public string StampNum { get; set; }

        public bool Valid { get; set; }

        public virtual GvaViewPerson Person { get; set; }
    }

    public class GvaViewPersonExaminerMap : EntityTypeConfiguration<GvaViewPersonExaminer>
    {
        public GvaViewPersonExaminerMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.LotId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.ExaminerCode)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.StampNum)
                .IsOptional()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonExaminers");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.ExaminerCode).HasColumnName("ExaminerCode");
            this.Property(t => t.StampNum).HasColumnName("StampNum");
            this.Property(t => t.Valid).HasColumnName("Valid");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithOptional(t => t.Examiner);
        }
    }
}
