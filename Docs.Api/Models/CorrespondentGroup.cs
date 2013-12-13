using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class CorrespondentGroup
    {
        public CorrespondentGroup()
        {
            this.Correspondents = new List<Correspondent>();
        }

        public int CorrespondentGroupId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<Correspondent> Correspondents { get; set; }
    }

    public class CorrespondentGroupMap : EntityTypeConfiguration<CorrespondentGroup>
    {
        public CorrespondentGroupMap()
        {
            // Primary Key
            this.HasKey(t => t.CorrespondentGroupId);

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
            this.ToTable("CorrespondentGroups");
            this.Property(t => t.CorrespondentGroupId).HasColumnName("CorrespondentGroupId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
