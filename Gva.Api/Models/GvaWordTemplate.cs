using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class GvaWordTemplate
    {
        public int GvaWordTemplateId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public byte[] Template { get; set; }
    }

    public class GvaWordTemplateMap : EntityTypeConfiguration<GvaWordTemplate>
    {
        public GvaWordTemplateMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaWordTemplateId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(200);

            this.Property(t => t.Description)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("GvaWordTemplates");
            this.Property(t => t.GvaWordTemplateId).HasColumnName("GvaWordTemplateId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.Description).HasColumnName("Description");
            this.Property(t => t.Template).HasColumnName("Template");
        }
    }
}
