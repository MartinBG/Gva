using System.Data.Entity.ModelConfiguration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Common.Api.Models
{
    public partial class NomValue
    {
        public int NomValueId { get; set; }

        public int NomId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public int? ParentValueId { get; set; }

        public string Alias { get; set; }

        private string textContentString;
        [JsonIgnore]
        public string TextContentString
        {
            get
            {
                return this.textContentString;
            }
            set
            {
                this.textContentString = value;
                this.textContent = null;
            }
        }

        private JContainer textContent;
        //use JContainer instead of JObject because entity framework throws an error
        //if a property type implements ICollection more than once and
        //ignoring it does not solve the problem
        //http://entityframework.codeplex.com/workitem/2014
        //https://entityframework.codeplex.com/workitem/2021
        public JContainer TextContent
        {
            get
            {
                if (this.textContent == null && !string.IsNullOrEmpty(this.TextContentString))
                {
                    this.textContent = JObject.Parse(this.TextContentString);
                }

                return this.textContent;
            }
        }

        public bool IsActive { get; set; }

        public int Order { get; set; }

        public string OldId { get; set; }

        public int? ParentNomId
        {
            get
            {
                return ParentValue != null ? ParentValue.NomId : (int?)null;
            }
        }

        [JsonIgnore]
        public virtual Nom Nom { get; set; }

        [JsonIgnore]
        public virtual NomValue ParentValue { get; set; }
    }

    public class NomValueMap : EntityTypeConfiguration<NomValue>
    {
        public NomValueMap()
        {
            // Primary Key
            this.HasKey(t => t.NomValueId);

            // Properties
            this.Property(t => t.Code)
                .HasMaxLength(500);

            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(500);

            this.Property(t => t.NameAlt)
                .HasMaxLength(500);

            this.Property(t => t.Alias)
                .HasMaxLength(50);

            this.Property(t => t.TextContentString)
                .IsOptional();

            this.Property(t => t.IsActive)
                .IsRequired();

            this.Property(t => t.Order)
                .IsRequired();

            // Table & Column Mappings
            this.ToTable("NomValues");
            this.Property(t => t.NomValueId).HasColumnName("NomValueId");
            this.Property(t => t.NomId).HasColumnName("NomId");
            this.Property(t => t.Code).HasColumnName("Code");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NameAlt).HasColumnName("NameAlt");
            this.Property(t => t.ParentValueId).HasColumnName("ParentValueId");
            this.Property(t => t.Alias).HasColumnName("Alias");
            this.Property(t => t.TextContentString).HasColumnName("TextContent");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
            this.Property(t => t.Order).HasColumnName("Order");

            // Relationships
            this.HasRequired(t => t.Nom)
                .WithMany(t => t.NomValues)
                .HasForeignKey(d => d.NomId);

            this.HasOptional(t => t.ParentValue)
                .WithMany()
                .HasForeignKey(d => d.ParentValueId);

            // Local-only properties
            this.Ignore(t => t.TextContent);
        }
    }
}
