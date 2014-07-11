using System;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class Ticket
    {
        public System.Guid TicketId { get; set; }

        public int? DocFileId { get; set; }

        public Guid? BlobOldKey { get; set; }

        public Guid? BlobNewKey { get; set; }

        public string DocTypeUri { get; set; }

        public Guid? AbbcdnKey { get; set; }

        public int? VisualizationMode { get; set; }
    }

    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketId);

            // Properties
            this.Property(t => t.DocTypeUri)
                .HasMaxLength(50);

            // Properties
            // Table & Column Mappings
            this.ToTable("Tickets");
            this.Property(t => t.TicketId).HasColumnName("TicketId");
            this.Property(t => t.DocFileId).HasColumnName("DocFileId");
            this.Property(t => t.BlobOldKey).HasColumnName("BlobOldKey");
            this.Property(t => t.BlobNewKey).HasColumnName("BlobNewKey");
            this.Property(t => t.DocTypeUri).HasColumnName("DocTypeUri");
            this.Property(t => t.AbbcdnKey).HasColumnName("AbbcdnKey");
            this.Property(t => t.VisualizationMode).HasColumnName("VisualizationMode");
        }
    }
}
