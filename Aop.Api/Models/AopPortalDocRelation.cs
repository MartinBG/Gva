using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;

namespace Aop.Api.Models
{
    public partial class AopPortalDocRelation
    {
        public int AopPortalDocRelationId { get; set; }
        public System.Guid PortalDocId { get; set; }
        public int DocId { get; set; }
        public byte[] Version { get; set; }
    }

    public class AopPortalDocRelationsMap : EntityTypeConfiguration<AopPortalDocRelation>
    {
        public AopPortalDocRelationsMap()
        {
            // Primary Key
            this.HasKey(t => t.AopPortalDocRelationId);

            // Table & Column Mappings
            this.ToTable("AopPortalDocRelations");
            this.Property(t => t.AopPortalDocRelationId).HasColumnName("AopPortalDocRelationId");
            this.Property(t => t.PortalDocId).HasColumnName("PortalDocId");
            this.Property(t => t.DocId).HasColumnName("DocId");
            this.Property(t => t.Version).HasColumnName("Version");
        }
    }
}
