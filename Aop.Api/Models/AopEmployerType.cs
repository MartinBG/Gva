using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Aop.Api.Models
{
    public partial class AopEmployerType
    {
        public AopEmployerType()
        {
            this.AopEmployers = new List<AopEmployer>();
        }

        public int AopEmployerTypeId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public bool IsActive { get; set; }
        public byte[] Version { get; set; }
        public virtual ICollection<AopEmployer> AopEmployers { get; set; }
    }

    public class AopEmployerTypeMap : EntityTypeConfiguration<AopEmployerType>
    {
        public AopEmployerTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.AopEmployerTypeId);

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
            this.ToTable("AopEmployerTypes");
            this.Property(t => t.AopEmployerTypeId).HasColumnName("AopEmployerTypeId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
