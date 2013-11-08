using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.Models
{
    public partial class TextBlob
    {
        public int TextBlobId { get; set; }
        public string Hash { get; set; }
        public int Size { get; set; }
        public string TextContent { get; set; }
    }

    public class TextBlobMap : EntityTypeConfiguration<TextBlob>
    {
        public TextBlobMap()
        {
            // Primary Key
            this.HasKey(t => t.TextBlobId);

            // Properties
            this.Property(t => t.Hash)
                .IsRequired()
                .HasMaxLength(50);

            this.Property(t => t.TextContent)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("TextBlobs");
            this.Property(t => t.TextBlobId).HasColumnName("TextBlobId");
            this.Property(t => t.Hash).HasColumnName("Hash");
            this.Property(t => t.Size).HasColumnName("Size");
            this.Property(t => t.TextContent).HasColumnName("TextContent");
        }
    }
}
