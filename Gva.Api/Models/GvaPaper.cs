using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.Api.Models
{
    public partial class GvaPaper
    {
        public int PaperId { get; set; }

        public string Name { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int FirstNumber { get; set; }

        public bool IsActive { get; set; }
    }

    public class GvaPaperMap : EntityTypeConfiguration<GvaPaper>
    {
        public GvaPaperMap()
        {
            // Primary Key
            this.HasKey(t => t.PaperId);

            // Properties
            this.Property(t => t.Name)
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("GvaPapers");
            this.Property(t => t.PaperId).HasColumnName("GvaPaperId");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.FromDate).HasColumnName("FromDate");
            this.Property(t => t.ToDate).HasColumnName("ToDate");
            this.Property(t => t.FirstNumber).HasColumnName("FirstNumber");
            this.Property(t => t.IsActive).HasColumnName("IsActive");
        }
    }
}