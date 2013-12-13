using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class CorrespondentType
    {
        public CorrespondentType()
        {
            this.Correspondents = new List<Correspondent>();
        }

        public int CorrespondentTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Correspondent> Correspondents { get; set; }
    }

    public class CorrespondentTypeMap : EntityTypeConfiguration<CorrespondentType>
    {
        public CorrespondentTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.CorrespondentTypeId);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(200);

            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("CorrespondentTypes");
            this.Property(t => t.CorrespondentTypeId).HasColumnName("CorrespondentTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
