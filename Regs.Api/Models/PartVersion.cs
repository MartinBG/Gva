using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Common.Api.Models;
using Common.Sequence;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Regs.Api.Models
{
    public partial class PartVersion
    {
        public static Sequence PartVersionSequence = new Sequence("partVersionSequence");

        private JObject content = null;

        public int PartVersionId { get; set; }

        public int PartId { get; set; }

        public string TextContent { get; set; }

        public int OriginalCommitId { get; set; }

        public int CreatorId { get; set; }

        public DateTime CreateDate { get; set; }

        public virtual Commit OriginalCommit { get; set; }

        public virtual PartOperation PartOperation { get; set; }

        public virtual Part Part { get; set; }

        public virtual User User { get; set; }

        public JObject Content
        {
            get
            {
                if (this.content == null)
                {
                    this.content = JObject.Parse(this.TextContent);
                }

                return this.content;
            }
            set
            {
                this.content = value;

                if (this.content == null)
                {
                    throw new ArgumentNullException("PartVersion.Content cannot be null, use DeletePart instead");
                }

                Formatting formatting;
#if DEBUG
                formatting = Formatting.Indented;
#else
                formatting = Formatting.None;
#endif
                this.TextContent = value.ToString(formatting);
            }
        }
    }

    public class PartVersionMap : EntityTypeConfiguration<PartVersion>
    {
        public PartVersionMap()
        {
            // Primary Key
            this.HasKey(t => t.PartVersionId);

            // Properties
            this.Property(t => t.PartVersionId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("LotPartVersions");
            this.Property(t => t.PartVersionId).HasColumnName("LotPartVersionId");
            this.Property(t => t.PartId).HasColumnName("LotPartId");
            this.Property(t => t.TextContent).HasColumnName("TextContent");
            this.Property(t => t.OriginalCommitId).HasColumnName("OriginalCommitId");
            this.Property(t => t.PartOperation).HasColumnName("LotPartOperationId");
            this.Property(t => t.CreatorId).HasColumnName("CreatorId");
            this.Property(t => t.CreateDate).HasColumnName("CreateDate");

            // Relationships
            this.HasRequired(t => t.OriginalCommit)
                .WithMany(t => t.ChangedPartVersions)
                .HasForeignKey(d => d.OriginalCommitId);

            this.HasRequired(t => t.Part)
                .WithMany(t => t.PartVersions)
                .HasForeignKey(d => d.PartId)
                .WillCascadeOnDelete();

            this.HasRequired(t => t.User)
                .WithMany()
                .HasForeignKey(d => d.CreatorId);

            // Local-only properties
            this.Ignore(t => t.Content);
        }
    }
}
