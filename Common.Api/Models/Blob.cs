using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Web.Helpers;
using Newtonsoft.Json;
using System;

namespace Common.Api.Models
{
    public partial class Blob
    {
        public int BlobId { get; set; }
        public System.Guid Key { get; set; }
        public string Hash { get; set; }
        public Nullable<int> Size { get; set; }
        public byte[] Content { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class BlobMap : EntityTypeConfiguration<Blob>
    {
        public BlobMap()
        {
            // Primary Key
            this.HasKey(t => t.BlobId);

            // Properties
            this.Property(t => t.Hash)
                .HasMaxLength(40);

            // Table & Column Mappings
            this.ToTable("Blobs");
            this.Property(t => t.BlobId).HasColumnName("BlobId");
            this.Property(t => t.Key).HasColumnName("Key");
            this.Property(t => t.Hash).HasColumnName("Hash");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.Content).HasColumnName("Content");
            this.Property(t => t.IsDeleted).HasColumnName("IsDeleted");
        }
    }
}
