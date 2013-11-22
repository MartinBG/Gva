using Common.Data;
using Common.Infrastructure;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Managers.LobManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

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

        public PartVersion AddPart(string path, JObject json, ILobManager lobManager, UserContext userContext)
        {
            SetPart setPart = this.Set.SetParts.FirstOrDefault(sp => sp.Path == path);
            if (setPart == null)
            {
                throw new Exception(string.Format("No LotSetPart in the current lot has the specified path: {0}", path));
            }

            return this.AddPart(setPart, json, lobManager, userContext);
        }

        public PartVersion AddPart(SetPart setPart, JObject json, ILobManager lobManager, UserContext userContext)
        {
            Commit currCommit = this.GetCommit();

            StringBuilder path = new StringBuilder();
            foreach (var symbol in setPart.Path)
            {
                path.Append(symbol == '*' ? (this.NextIndex++).ToString() : symbol.ToString());
            }
            string pathStr = path.ToString();

            if (currCommit.PartVersions.Any(pv => pv.Part.Path == pathStr))
            {
                throw new Exception(string.Format("Specified path ({0}) is already in index", path.ToString()));
            }

            Part part = this.Parts.FirstOrDefault(p => p.Path == pathStr);
            if (part == null)
            {
                part = new Part
                {
                    SetPart = setPart,
                    LotId = this.LotId,
                    Path = pathStr
                };
                this.Parts.Add(part);
            }

            PartVersion partVersion = new PartVersion
            {
                OriginalCommitId = currCommit.CommitId,
                PartOperation = PartOperation.Add,
                CreatorId = userContext.UserId,
                CreateDate = DateTime.Now,
                Part = part,
                TextBlob = lobManager.AddLob(json.ToString())
            };
            currCommit.PartVersions.Add(partVersion);

            Events.Raise(new LotEvent(LotOperation.AddPart, this, new List<PartVersion>() { partVersion }));

            return partVersion;
        }

        public PartVersion UpdatePart(string path, JObject json, ILobManager lobManager, UserContext userContext)
        {
            PartVersion partVersion = this.GetPart(path);

            return this.UpdatePartVersion(partVersion, json, lobManager, userContext);
        }

        public PartVersion UpdatePart(Part part, JObject json, ILobManager lobManager, UserContext userContext)
        {
            Commit currCommit = this.GetCommit();
            PartVersion partVersion = currCommit.PartVersions.FirstOrDefault(pv => pv.Part.PartId == part.PartId && pv.PartOperation != PartOperation.Delete);

            if (partVersion == null)
            {
                throw new Exception(string.Format("No LotPart found having the specified part with id {1}", part.PartId));
            }

            return this.UpdatePartVersion(partVersion, json, lobManager, userContext);
        }

        public PartVersion DeletePart(string path, UserContext userContext)
        {
            PartVersion partVersion = this.GetPart(path);

            return this.DeletePartVersion(partVersion, userContext);
        }

        public PartVersion DeletePart(Part part, UserContext userContext)
        {
            Commit currCommit = this.GetCommit();
            PartVersion partVersion = currCommit.PartVersions.FirstOrDefault(pv => pv.Part.PartId == part.PartId && pv.PartOperation != PartOperation.Delete);

            if (partVersion == null)
            {
                throw new Exception(string.Format("No LotPart found having the spcified part with id {1}", part.PartId));
            }

            return this.DeletePartVersion(partVersion, userContext);
        }

        public PartVersion ResetPart(string path)
        {
            Commit currCommit = this.GetCommit();

            PartVersion partVersion = currCommit.PartVersions
                .FirstOrDefault(pv => 
                    pv.Part.Path == path &&
                    pv.OriginalCommitId == currCommit.CommitId);

            if (partVersion == null)
            {
                throw new Exception("No changes to reset!");
            }

            Part part = partVersion.Part;

            part.PartVersions.Remove(partVersion);

            if (part.PartVersions.Count == 0)
            {
                this.Parts.Remove(part);
            }

            PartVersion prevPartVersion = currCommit.ParentCommit.PartVersions.FirstOrDefault(pv => pv.Part.Path == path && pv.PartOperation != PartOperation.Delete);
            if (prevPartVersion != null)
            {
                currCommit.PartVersions.Add(prevPartVersion);
            }

            Events.Raise(new LotEvent(LotOperation.ResetPart, this, new List<PartVersion>() { partVersion }));

            return partVersion;
        }

        public void Reset(int commmitId)
        {
            Commit currCommit = this.GetCommit();
            bool partVersionsInIndex = currCommit.PartVersions
                .Any(pv =>
                    pv.PartOperation != PartOperation.Delete &&
                    pv.OriginalCommitId == currCommit.CommitId);

            if (partVersionsInIndex)
            {
                throw new Exception("Cannot reset with uncommited changes in index");
            }

            Commit newIndex = this.Commits.FirstOrDefault(c => c.CommitId == commmitId);
            if (newIndex == null)
            {
                throw new Exception(string.Format("Invalid commitId: {}", commmitId));
            }

            newIndex.IsIndex = true;

            IEnumerable<PartVersion> deletedPartVersions = new List<PartVersion>();
            IList<Commit> commitsToDelete = this.Commits.Where(c => c.CommitId > commmitId).ToList();

            foreach (var commit in commitsToDelete)
            {
                IEnumerable<PartVersion> partVersionsToDelete = commit.PartVersions.Where(pv => pv.OriginalCommitId == commit.CommitId);
                deletedPartVersions = deletedPartVersions.Concat<PartVersion>(partVersionsToDelete);

                foreach (var partVersion in partVersionsToDelete)
                {
                    Part part = partVersion.Part;
                    part.PartVersions.Remove(partVersion);

                    if (part.PartVersions.Count == 0)
                    {
                        this.Parts.Remove(part);
                    }
                }

                this.Commits.Remove(commit);
            }

            Events.Raise(new LotEvent(LotOperation.Reset, this, deletedPartVersions.ToList()));
        }

        public void Commit(UserContext userContext, IList<string> paths = null)
        {
            Commit currCommit = this.GetCommit();

            Commit index = new Commit
            {
                ParentCommitId = currCommit.CommitId,
                CommiterId = userContext.UserId,
                CommitDate = DateTime.Now,
                PartVersions = new Collection<PartVersion>(this.GetParts().ToList())
            };

            IEnumerable<PartVersion> changedPartVersions;

            if (paths != null)
            {
                changedPartVersions = currCommit.PartVersions.Where(pv => paths.Contains(pv.Part.Path));

                if (changedPartVersions.Count() != paths.Count)
                {
                    throw new Exception("Invalid path!");
                }

                if (changedPartVersions.Any(pv => pv.OriginalCommitId != currCommit.CommitId))
                {
                    throw new Exception("Invalid Path!");
                }

                IEnumerable<PartVersion> deletedPVsNotForCommit = currCommit.PartVersions
                    .Where(pv => pv.PartOperation == PartOperation.Delete && !changedPartVersions.Contains(pv));
                IList<PartVersion> partVersions = index.PartVersions.Concat(deletedPVsNotForCommit).ToList();
                index.PartVersions = new Collection<PartVersion>(partVersions);

                PartVersion[] indexPartVersions = currCommit.PartVersions
                    .Where(pv => pv.OriginalCommitId == currCommit.CommitId && !changedPartVersions.Contains(pv))
                    .ToArray();
                foreach (var partVersion in indexPartVersions)
                {
                    partVersion.OriginalCommit = index;
                    currCommit.PartVersions.Remove(partVersion);
                }
            }
            else
            {
                changedPartVersions = currCommit.PartVersions.Where(pv => pv.OriginalCommitId == currCommit.CommitId);
            }

            index.IsIndex = true;
            this.Commits.Add(index);

            currCommit.CommitDate = DateTime.Now;
            currCommit.CommiterId = userContext.UserId;
            currCommit.IsIndex = false;

            Events.Raise(new LotEvent(LotOperation.Commit, this, changedPartVersions.ToList()));
        }

        public IEnumerable<PartVersion> GetParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Add, PartOperation.Update }, commitId);
        }

        public IEnumerable<PartVersion> GetAddedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Add }, commitId);
        }

        public IEnumerable<PartVersion> GetUpdatedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Update }, commitId);
        }

        public IEnumerable<PartVersion> GetDeletedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Delete }, commitId);
        }

        public PartVersion GetPart(string path, int? commitId = null)
        {
            Commit commit = this.GetCommit(commitId);
            PartVersion partVersion = commit.PartVersions.FirstOrDefault(pv => pv.Part.Path == path && pv.PartOperation != PartOperation.Delete);

            if (partVersion == null)
            {
                throw new Exception(string.Format("Invalid path: {0}", path));
            }

            return partVersion;
        }

        private PartVersion UpdatePartVersion(PartVersion partVersion, JObject json, ILobManager lobManager, UserContext userContext)
        {
            Commit currCommit = this.GetCommit();

            if (partVersion.OriginalCommitId == currCommit.CommitId)
            {
                partVersion.TextBlob = lobManager.AddLob(json.ToString());
                partVersion.CreatorId = userContext.UserId;
                partVersion.CreateDate = DateTime.Now;
                return partVersion;
            }

            PartVersion updatedPartVersion = new PartVersion
            {
                OriginalCommitId = currCommit.CommitId,
                PartOperation = PartOperation.Update,
                CreatorId = userContext.UserId,
                CreateDate = DateTime.Now,
                Part = partVersion.Part,
                TextBlob = lobManager.AddLob(json.ToString())
            };
            currCommit.PartVersions.Remove(partVersion);
            currCommit.PartVersions.Add(updatedPartVersion);

            Events.Raise(new LotEvent(LotOperation.UpdatePart, this, new List<PartVersion>() { partVersion }));

            return updatedPartVersion;
        }

        private PartVersion DeletePartVersion(PartVersion partVersion, UserContext userContext)
        {
            Commit currCommit = this.GetCommit();

            if (partVersion.OriginalCommitId == currCommit.CommitId)
            {
                throw new Exception("Cannot delete partVersion added in the same commit!");
            }

            PartVersion deletedPartVersion = new PartVersion
            {
                OriginalCommitId = currCommit.CommitId,
                PartOperation = PartOperation.Delete,
                CreatorId = userContext.UserId,
                CreateDate = DateTime.Now,
                Part = partVersion.Part,
                TextBlob = partVersion.TextBlob
            };
            currCommit.PartVersions.Remove(partVersion);
            currCommit.PartVersions.Add(deletedPartVersion);

            Events.Raise(new LotEvent(LotOperation.DeletePart, this, new List<PartVersion>() { partVersion }));

            return deletedPartVersion;
        }

        private IEnumerable<PartVersion> GetPartsByOperations(PartOperation[] partOperations, int? commitId)
        {
            Commit commit = this.GetCommit(commitId);
            IEnumerable<PartVersion> partVersions = commit.PartVersions.Where(pv => partOperations.Contains(pv.PartOperation));

            return partVersions;
        }

        private Commit GetCommit(int? commitId = null)
        {
            Commit commit = commitId.HasValue ?
                this.Commits.FirstOrDefault(c => c.CommitId == commitId) :
                this.Commits.FirstOrDefault(c => c.IsIndex == true);

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
