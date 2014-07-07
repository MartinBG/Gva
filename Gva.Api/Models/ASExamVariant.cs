using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class ASExamVariant
    {
        public ASExamVariant()
        {
            this.ASExamVariantQuestions = new List<ASExamVariantQuestion>();
        }

        public int ASExamVariantId { get; set; }
        public string Name { get; set; }
        public int ASExamQuestionTypeId { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<ASExamVariantQuestion> ASExamVariantQuestions { get; set; }
    }

    public class ASExamVariantMap : EntityTypeConfiguration<ASExamVariant>
    {
        public ASExamVariantMap()
        {
            // Primary Key
            this.HasKey(t => t.ASExamVariantId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ASExamVariants");
            this.Property(t => t.ASExamVariantId).HasColumnName("ASExamVariantId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.ASExamQuestionTypeId).HasColumnName("ASExamQuestionTypeId");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
