using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Newtonsoft.Json.Schema;

namespace Regs.Api.Models
{
    public partial class Schema
    {
        public Schema()
        {
            this.Sets = new List<Set>();
            this.SetParts = new List<SetPart>();
        }

        private JsonSchema jsonSchema;

        public int SchemaId { get; set; }

        public string SchemaText { get; set; }

        public JsonSchema JsonSchema
        {
            get
            {
                return jsonSchema;
            }
            set
            {
                this.jsonSchema = value;
            }
        }

        public virtual ICollection<Set> Sets { get; set; }

        public virtual ICollection<SetPart> SetParts { get; set; }
    }

    public class SchemaMap : EntityTypeConfiguration<Schema>
    {
        public SchemaMap()
        {
            // Primary Key
            this.HasKey(t => t.SchemaId);

            // Properties
            this.Property(t => t.SchemaId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.SchemaText)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("LotSchemas");
            this.Property(t => t.SchemaId).HasColumnName("LotSchemaId");
            this.Property(t => t.SchemaText).HasColumnName("SchemaText");

            // Local-only properties
            this.Ignore(t => t.JsonSchema);
        }
    }
}
