using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Sequence;

namespace Regs.Api.Models
{
    public partial class Set
    {
        public Set()
        {
            this.SetParts = new List<SetPart>();
            this.Schemas = new List<Schema>();
        }

        public int SetId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public virtual ICollection<SetPart> SetParts { get; set; }

        public virtual ICollection<Schema> Schemas { get; set; }
    }

    public class SetMap : EntityTypeConfiguration<Set>
    {
        public SetMap()
        {
            // Primary Key
            this.HasKey(t => t.SetId);

            // Properties
            this.Property(t => t.SetId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("LotSets");
            this.Property(t => t.SetId).HasColumnName("LotSetId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");

            this.HasMany(t => t.Schemas)
                .WithMany(t => t.Sets)
                .Map(m =>
                {
                    m.ToTable("LotSetSchemas");
                    m.MapLeftKey("LotSetId");
                    m.MapRightKey("LotSchemaId");
                });
        }
    }
}
