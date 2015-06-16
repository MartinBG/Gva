using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Common.Api.Models
{
    public partial class Nom
    {
        public Nom()
        {
            this.NomValues = new List<NomValue>();
        }

        public int NomId { get; set; }

        public string Name { get; set; }

        public string Alias { get; set; }

        public string Category { get; set; }

        public virtual ICollection<NomValue> NomValues { get; set; }
    }

    public class NomMap : EntityTypeConfiguration<Nom>
    {
        public NomMap()
        {
            // Primary Key
            this.HasKey(t => t.NomId);

            // Properties
            this.Property(t => t.NomId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(55);

            this.Property(t => t.Alias)
                .IsRequired()
                .HasMaxLength(55);

            // Table & Column Mappings
            this.ToTable("Noms");
            this.Property(t => t.NomId).HasColumnName("NomId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.Category).HasColumnName("Category");
        }
    }
}
