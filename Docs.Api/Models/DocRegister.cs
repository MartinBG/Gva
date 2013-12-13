using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class DocRegister
    {
        public DocRegister()
        {
            this.Docs = new List<Doc>();
        }

        public int DocRegisterId { get; set; }
        public Nullable<int> RegisterIndexId { get; set; }
        public string Alias { get; set; }
        public int CurrentNumber { get; set; }
        public byte[] Version { get; set; }
        public virtual RegisterIndex RegisterIndex { get; set; }
        public virtual ICollection<Doc> Docs { get; set; }
    }

    public class DocRegisterMap : EntityTypeConfiguration<DocRegister>
    {
        public DocRegisterMap()
        {
            // Primary Key
            this.HasKey(t => t.DocRegisterId);

            // Properties
            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("DocRegisters");
            this.Property(t => t.DocRegisterId).HasColumnName("DocRegisterId");
            this.Property(t => t.RegisterIndexId).HasColumnName("RegisterIndexId");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.CurrentNumber).HasColumnName("CurrentNumber");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasOptional(t => t.RegisterIndex)
                .WithMany(t => t.DocRegisters)
                .HasForeignKey(d => d.RegisterIndexId);

        }
    }
}
