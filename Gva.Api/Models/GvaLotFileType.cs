using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class GvaLotFileType
    {
        public GvaLotFileType()
        {
            this.GvaLotFiles = new List<GvaLotFile>();
        }

        public int GvaLotFileTypeId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public virtual ICollection<GvaLotFile> GvaLotFiles { get; set; }
    }

    public class GvaLotFileTypeMap : EntityTypeConfiguration<GvaLotFileType>
    {
        public GvaLotFileTypeMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaLotFileTypeId);

            // Properties
            this.Property(t => t.GvaLotFileTypeId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Code)
                .HasMaxLength(50);

            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaLotFileTypes");
            this.Property(t => t.GvaLotFileTypeId).HasColumnName("GvaLotFileTypeId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
        }
    }
}
