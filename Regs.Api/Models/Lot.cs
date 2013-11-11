using Common.Data;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Extensions;

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

        public PartVersion AddPart(string path, JObject json)
        {
            SetPart setPart = this.Set.SetParts.FirstOrDefault(sp => sp.Path == path);
            if (setPart == null)
            {
                throw new Exception("Cannot construct path from the specified schema.");
            }

            return this.AddPart(setPart, json);
        }

        public PartVersion AddPart(SetPart setPart, JObject json)
        {
            IUnitOfWork unitOfWork = ServiceLocator.Current.GetInstance<IUnitOfWork>();
            Commit currCommit = this.GetCommit();

            string path = string.Empty;
            foreach (var symbol in setPart.Path)
            {
                path += (symbol == '*' ? (this.NextIndex++).ToString() : symbol.ToString());
            }

            Part newPart = new Part
            {
                SetPartId = setPart.SetPartId,
                LotId = this.LotId,
                Path = path
            };
            unitOfWork.DbContext.Set<Part>().Add(newPart);

            string content = json.ToString();
            string hash = content.CalculateSHA1();
            TextBlob textBlob = unitOfWork.DbContext.Set<TextBlob>().FirstOrDefault(tb => tb.Hash == hash);

            if (textBlob == null)
            {
                textBlob = new TextBlob
                {
                    Hash = hash,
                    Size = content.Length,
                    TextContent = content
                };
                unitOfWork.DbContext.Set<TextBlob>().Add(textBlob);
            }

            PartVersion partVersion = new PartVersion
            {
                OriginalCommitId = currCommit.CommitId,
                PartOperationId = (int)PartOperation.Add,
                CreatorId = 1, //TO DO - user context
                CreateDate = DateTime.Now,
                Part = newPart,
                TextBlobId = textBlob.TextBlobId
            };
            currCommit.PartVersions.Add(partVersion);

            return partVersion;
        }

        public IEnumerable<PartVersion> GetParts(int? commitId = null)
        {
            return this.GetPartsByOperations( new PartOperation[] { PartOperation.Add, PartOperation.Update }, commitId);
        }

        public IEnumerable<PartVersion> GetAddedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Add }, commitId);
        }

        public IEnumerable<PartVersion> GetUpdatedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Update }, commitId);
        }

        public IEnumerable<PartVersion> DeletedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Delete }, commitId);
        }

        public PartVersion GetPart(string path, int? commitId)
        {
            Commit commit = this.GetCommit(commitId);
            PartVersion partVersion = commit.PartVersions.FirstOrDefault(pv => pv.Part.Path == path && pv.PartOperationId != (int)PartOperation.Delete);

            if (partVersion == null)
            {
                throw new Exception(string.Format("Invalid path: {0}", path));
            }

            return partVersion;
        }

        private IEnumerable<PartVersion> GetPartsByOperations(PartOperation[] partOperations, int? commitId)
        {
            IEnumerable<int> partOperationIds = partOperations.Select(po => (int)po);
            Commit commit = GetCommit(commitId);
            IEnumerable<PartVersion> partVersions = commit.PartVersions.Where(pv => partOperationIds.Contains(pv.PartOperationId));

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
