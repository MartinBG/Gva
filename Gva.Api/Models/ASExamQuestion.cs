using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class ASExamQuestion
    {
        public ASExamQuestion()
        {
            this.ASExamVariantQuestions = new List<ASExamVariantQuestion>();
        }

        public int ASExamQuestionId { get; set; }
        public int ASExamQuestionTypeId { get; set; }
        public string QuestionText { get; set; }
        public string Answer1 { get; set; }
        public bool IsChecked1 { get; set; }
        public string Answer2 { get; set; }
        public bool IsChecked2 { get; set; }
        public string Answer3 { get; set; }
        public bool IsChecked3 { get; set; }
        public string Answer4 { get; set; }
        public bool IsChecked4 { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<ASExamVariantQuestion> ASExamVariantQuestions { get; set; }
    }

    public class ASExamQuestionMap : EntityTypeConfiguration<ASExamQuestion>
    {
        public ASExamQuestionMap()
        {
            // Primary Key
            this.HasKey(t => t.ASExamQuestionId);

            // Properties
            this.Property(t => t.QuestionText)
                .IsRequired();

            this.Property(t => t.Answer1)
                .IsRequired();

            this.Property(t => t.Answer2)
                .IsRequired();

            this.Property(t => t.Answer3)
                .IsRequired();

            this.Property(t => t.Answer4)
                .IsRequired();

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("ASExamQuestions");
            this.Property(t => t.ASExamQuestionId).HasColumnName("ASExamQuestionId");
            this.Property(t => t.ASExamQuestionTypeId).HasColumnName("ASExamQuestionTypeId");
            this.Property(t => t.QuestionText).HasColumnName("QuestionText");
            this.Property(t => t.Answer1).HasColumnName("Answer1");
            this.Property(t => t.IsChecked1).HasColumnName("IsChecked1");
            this.Property(t => t.Answer2).HasColumnName("Answer2");
            this.Property(t => t.IsChecked2).HasColumnName("IsChecked2");
            this.Property(t => t.Answer3).HasColumnName("Answer3");
            this.Property(t => t.IsChecked3).HasColumnName("IsChecked3");
            this.Property(t => t.Answer4).HasColumnName("Answer4");
            this.Property(t => t.IsChecked4).HasColumnName("IsChecked4");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
