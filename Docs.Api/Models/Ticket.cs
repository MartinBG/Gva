using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Docs.Api.Models
{
    public partial class Ticket
    {
        public System.Guid TicketId { get; set; }
        public int DocFileId { get; set; }
        public System.Guid OldKey { get; set; }
        public Nullable<System.Guid> NewKey { get; set; }
        public Nullable<int> VisualizationMode { get; set; }
    }

    public class TicketMap : EntityTypeConfiguration<Ticket>
    {
        public TicketMap()
        {
            // Primary Key
            this.HasKey(t => t.TicketId);

            // Properties
            // Table & Column Mappings
            this.ToTable("Tickets");
            this.Property(t => t.TicketId).HasColumnName("TicketId");
            this.Property(t => t.DocFileId).HasColumnName("DocFileId");
            this.Property(t => t.OldKey).HasColumnName("OldKey");
            this.Property(t => t.NewKey).HasColumnName("NewKey");
            this.Property(t => t.VisualizationMode).HasColumnName("VisualizationMode");
        }
    }
}
