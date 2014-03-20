using System.Data.Entity.ModelConfiguration;

namespace Gva.Api.Models
{
    public partial class GvaLotObject
    {
        public int GvaLotObjectId { get; set; }

        public int? GvaApplicationId { get; set; }

        public int LotPartId { get; set; }

        public bool IsActive { get; set; }

        public virtual GvaApplication GvaApplication { get; set; }

        public virtual Regs.Api.Models.Part LotPart { get; set; }
    }

    public class GvaLotObjectMap : EntityTypeConfiguration<GvaLotObject>
    {
        public GvaLotObjectMap()
        {
            // Primary Key
            this.HasKey(t => t.GvaLotObjectId);

            // Properties
            // Table & Column Mappings
            this.ToTable("GvaLotObjects");
            this.Property(t => t.GvaLotObjectId).HasColumnName("GvaLotObjectId");
            this.Property(t => t.GvaApplicationId).HasColumnName("GvaApplicationId");
            this.Property(t => t.LotPartId).HasColumnName("LotPartId");
            this.Property(t => t.IsActive).HasColumnName("IsActive");

            // Relationships
            this.HasOptional(t => t.GvaApplication)
                .WithMany(t => t.GvaLotObjects)
                .HasForeignKey(d => d.GvaApplicationId);
            this.HasRequired(t => t.LotPart)
                .WithMany()
                .HasForeignKey(d => d.LotPartId);
        }
    }
}
