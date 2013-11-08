using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Regs.Api.Models
{
    public partial class Lot
    {
        public Lot()
        {
            this.Commits = new List<Commit>();
            this.Parts = new List<Part>();
        }

        public int LotId { get; set; }
        public int SetId { get; set; }
        public int NextIndex { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
        public virtual Set Set { get; set; }

        //public void AddPart(string schema, JObject json)
        //{
        //    Commit lastCommit = this.GetCommit();
        //    SetPart setPart = this.Set.SetParts.FirstOrDefault(sp => sp.Path == schema);

        //    if (setPart == null)
        //    {
        //        throw new Exception("Cannot construct path from the specified schema.");
        //    }

        //    string path = string.Empty;
        //    foreach(var symbol in schema)
        //    {
        //        path += (symbol == '*' ? this.NextIndex++ : symbol);
        //    }

        //    Part newPart = new Part
        //    {
        //        SetPartId = setPart.SetPartId,
        //        LotId = this.LotId,
        //        Path = path
        //    };
        //}

        public IEnumerable<PartVersion> GetParts(int? commitId = null)
        {
            return this.GetPartsByOperations( new string[] {"Add", "Update"}, commitId);
        }

        public IEnumerable<PartVersion> GetAddedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new string[] { "Add" }, commitId);
        }

        public IEnumerable<PartVersion> GetUpdatedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new string[] { "Update" }, commitId);
        }

        public IEnumerable<PartVersion> DeletedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new string[] { "Delete" }, commitId);
        }

        public PartVersion GetPart(string path, int? commitId)
        {
            Commit commit = this.GetCommit(commitId);
            PartVersion partVersion = commit.PartVersions.FirstOrDefault(pv => pv.Part.Path == path && pv.PartOperation.Alias != "Delete");

            if (partVersion == null)
            {
                throw new Exception(string.Format("Invalid path: {0}", path));
            }

            return partVersion;
        }

        private IEnumerable<PartVersion> GetPartsByOperations(string[] partOperations, int? commitId)
        {
            Commit commit = GetCommit(commitId);
            IEnumerable<PartVersion> partVersions = commit.PartVersions.Where(pv => partOperations.Contains(pv.PartOperation.Alias));

            return partVersions;
        }

        private Commit GetCommit(int? commitId = null)
        {
            Commit commit = commitId.HasValue ?
                this.Commits.FirstOrDefault(c => c.CommitId == commitId) :
                this.Commits.OrderByDescending(c => c.CommitDate).FirstOrDefault();

            if (commit == null)
            {
                throw new Exception(string.Format("Invalid commitId: {0}", commitId));
            }

            return commit;
        }
    }

    public class LotMap : EntityTypeConfiguration<Lot>
    {
        public LotMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Table & Column Mappings
            this.ToTable("Lots");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.SetId).HasColumnName("LotSetId");
            this.Property(t => t.NextIndex).HasColumnName("NextIndex");

            // Relationships
            this.HasRequired(t => t.Set)
                .WithMany(t => t.Lots)
                .HasForeignKey(d => d.SetId);
        }
    }
}
