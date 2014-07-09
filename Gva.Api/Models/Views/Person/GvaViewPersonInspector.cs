using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;

namespace Gva.Api.Models.Views.Person
{
    public partial class GvaViewPersonInspector
    {
        public int LotId { get; set; }

        public string ExaminerCode { get; set; }

        public int CaaId { get; set; }

        public string StampNum { get; set; }

        public bool Valid { get; set; }

        public virtual NomValue Caa { get; set; }

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

            this.Property(t => t.CaaId)
                .IsRequired();

            this.Property(t => t.StampNum)
                .IsOptional()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaViewPersonInspectors");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.ExaminerCode).HasColumnName("ExaminerCode");
            this.Property(t => t.CaaId).HasColumnName("CaaId");
            this.Property(t => t.StampNum).HasColumnName("StampNum");
            this.Property(t => t.Valid).HasColumnName("Valid");

            // Relationships
            this.HasRequired(t => t.Person)
                .WithOptional(t => t.Inspector);
            this.HasRequired(t => t.Caa)
                .WithMany()
                .HasForeignKey(t => t.CaaId);
        }
    }
}
