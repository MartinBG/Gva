using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class ASExamVariantQuestion
    {
        public int ASExamVariantQuestionId { get; set; }
        public int ASExamVariantId { get; set; }
        public int ASExamQuestionId { get; set; }
        public int QuestionNumber { get; set; }
        public virtual ASExamQuestion ASExamQuestion { get; set; }
        public virtual ASExamVariant ASExamVariant { get; set; }
    }

    public class ASExamVariantQuestionMap : EntityTypeConfiguration<ASExamVariantQuestion>
    {
        public ASExamVariantQuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.ASExamVariantQuestionId);

            // Properties
            // Table & Column Mappings
            this.ToTable("ASExamVariantQuestions");
            this.Property(t => t.ASExamVariantQuestionId).HasColumnName("ASExamVariantQuestionId");
            this.Property(t => t.ASExamVariantId).HasColumnName("ASExamVariantId");
            this.Property(t => t.ASExamQuestionId).HasColumnName("ASExamQuestionId");
            this.Property(t => t.QuestionNumber).HasColumnName("QuestionNumber");

            // Relationships
            this.HasRequired(t => t.ASExamQuestion)
                .WithMany(t => t.ASExamVariantQuestions)
                .HasForeignKey(d => d.ASExamVariantId);
            this.HasRequired(t => t.ASExamVariant)
                .WithMany(t => t.ASExamVariantQuestions)
                .HasForeignKey(d => d.ASExamQuestionId);

        }
    }
}
