using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class RegisterIndex
    {
        public RegisterIndex()
        {
            this.Correspondents = new List<Correspondent>();
            this.DocRegisters = new List<DocRegister>();
            this.DocTypes = new List<DocType>();
            this.DocTypes1 = new List<DocType>();
        }

        public int RegisterIndexId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Code { get; set; }

        public string NumberFormat { get; set; }

        public bool IsActive { get; set; }

        public byte[] Version { get; set; }

        public virtual ICollection<Correspondent> Correspondents { get; set; }

        public virtual ICollection<DocRegister> DocRegisters { get; set; }

        public virtual ICollection<DocType> DocTypes { get; set; }

        public virtual ICollection<DocType> DocTypes1 { get; set; }
    }

    public class RegisterIndexMap : EntityTypeConfiguration<RegisterIndex>
    {
        public RegisterIndexMap()
        {
            // Primary Key
            this.HasKey(t => t.RegisterIndexId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Code)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.NumberFormat)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("RegisterIndexes");
            this.Property(t => t.RegisterIndexId).HasColumnName("RegisterIndexId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.NumberFormat).HasColumnName("NumberFormat");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
