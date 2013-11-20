using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.Models
{
    public partial class NomValue
    {
        public int NomValueId { get; set; }
        public int NomId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameAlt { get; set; }
        public int? ParentValueId { get; set; }
        public string Alias { get; set; }
        public string TextContent { get; set; }
        public bool IsActive { get; set; }

        public virtual Nom Nom { get; set; }
        public virtual NomValue ParentValue { get; set; }
    }

    public class NomValueMap : EntityTypeConfiguration<NomValue>
    {
        public NomValueMap()
        {
            // Primary Key
            this.HasKey(t => t.NomValueId);

            // Properties
            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.NameAlt)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.Alias)
                .HasMaxLength(50);

            this.Property(t => t.TextContent)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("NomValues");
            this.Property(t => t.NomValueId).HasColumnName("NomValueId");
            this.Property(t => t.NomId).HasColumnName("NomId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.ParentValueId).HasColumnName("ParentValueId");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.TextContent).HasColumnName("TextContent");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasRequired(t => t.Nom)
                .WithMany(t => t.NomValues)
                .HasForeignKey(d => d.NomId);

            this.HasOptional(t => t.ParentValue)
                .WithMany()
                .HasForeignKey(d => d.ParentValueId);
        }
    }
}
