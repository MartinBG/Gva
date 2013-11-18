using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Common.Infrastructure;
using Newtonsoft.Json.Linq;
using Regs.Api.Managers.LobManager;

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

            if (currCommit.PartVersions.Any(pv => pv.Part.Path == path.ToString() && pv.PartOperation != PartOperation.Delete))
            {
                throw new Exception(string.Format("Specified path ({0}) is already in index", path.ToString()));
            }

            Part newPart = new Part
            {
                SetPart = setPart,
                LotId = this.LotId,
                Path = path.ToString()
            };
            this.Parts.Add(newPart);

            PartVersion partVersion = new PartVersion
            {
                OriginalCommitId = currCommit.CommitId,
                PartOperation = PartOperation.Add,
                CreatorId = userContext.UserId,
                CreateDate = DateTime.Now,
                Part = newPart,
                TextBlob = lobManager.AddLob(json.ToString())
            };
            currCommit.PartVersions.Add(partVersion);

            return partVersion;
        }

        public PartVersion UpdatePart(string path, JObject json, ILobManager lobManager, UserContext userContext)
        {
            PartVersion partVersion = this.GetPart(path);

            return this.UpdatePartVersion(partVersion, json, lobManager, userContext);
        }

        public PartVersion UpdatePart(SetPart setPart, JObject json, ILobManager lobManager, UserContext userContext)
        {
            Commit currCommit = this.GetCommit();
            PartVersion partVersion = currCommit.PartVersions.FirstOrDefault(pv => pv.Part.SetPartId == setPart.SetPartId && pv.PartOperation != PartOperation.Delete);

            if (partVersion == null)
            {
                throw new Exception(string.Format("No LotPart found having the spcified setPart with id {1}", setPart.SetPartId));
            }

            return this.UpdatePartVersion(partVersion, json, lobManager, userContext);
        }

        public PartVersion DeletePart(string path, UserContext userContext)
        {
            PartVersion partVersion = this.GetPart(path);

            return this.DeletePartVersion(partVersion, userContext);
        }

        public PartVersion DeletePart(SetPart setPart, UserContext userContext)
        {
            Commit currCommit = this.GetCommit();
            PartVersion partVersion = currCommit.PartVersions.FirstOrDefault(pv => pv.Part.SetPartId == setPart.SetPartId && pv.PartOperation != PartOperation.Delete);

            if (partVersion == null)
            {
                throw new Exception(string.Format("No LotPart found having the spcified setPart with id {1}", setPart.SetPartId));
            }

            return this.DeletePartVersion(partVersion, userContext);
        }

        public PartVersion ResetPart(string path)
        {
            Commit currCommit = this.GetCommit();

            PartVersion partVersion = currCommit.PartVersions
                .FirstOrDefault(pv => 
                    pv.Part.Path == path &&
                    pv.PartOperation != PartOperation.Delete &&
                    pv.OriginalCommitId == currCommit.CommitId);

            if (partVersion == null)
            {
                throw new Exception("No changes to reset!");
            }

            Part part = partVersion.Part;
            if (part.PartVersions.Count == 1)
            {
                this.Parts.Remove(part);
                return partVersion;
            }

            part.PartVersions.Remove(partVersion);

            if (partVersion.PartOperation == PartOperation.Update)
            {
                PartVersion prevPartVersion = this.GetPart(path, currCommit.ParentCommitId);
                currCommit.PartVersions.Add(prevPartVersion);
            }

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

            IList<Commit> commitsToDelete = this.Commits.Where(c => c.CommitId > commmitId).ToList();
            foreach (var commit in commitsToDelete)
            {
                IEnumerable<PartVersion> partVersionsToDelete = commit.PartVersions.Where(pv => pv.OriginalCommitId == commit.CommitId);

                foreach (var partVersion in partVersionsToDelete)
                {
                    Part part = partVersion.Part;
                    if (part.PartVersions.Count == 1)
                    {
                        this.Parts.Remove(part);
                    }
                    else 
                    {
                        part.PartVersions.Remove(partVersion);
                    }
                }

                this.Commits.Remove(commit);
            }
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

            if (paths != null)
            {
                IEnumerable<PartVersion> partVersionsToCommit = currCommit.PartVersions.Where(pv => paths.Contains(pv.Part.Path));

                if (partVersionsToCommit.Count() != paths.Count)
                {
                    throw new Exception("Invalid path!");
                }

                if (partVersionsToCommit.Any(pv => pv.OriginalCommitId != currCommit.CommitId))
                {
                    throw new Exception("Invalid Path!");
                }

                PartVersion[] modifiedAndNewPartVersions = this.GetParts().Where(pv => pv.OriginalCommitId == currCommit.CommitId && !partVersionsToCommit.Contains(pv)).ToArray();
                foreach (var partVersion in modifiedAndNewPartVersions)
                {
                    partVersion.OriginalCommit = index;
                    currCommit.PartVersions.Remove(partVersion);
                }
            }

            index.IsIndex = true;
            this.Commits.Add(index);

            currCommit.CommitDate = DateTime.Now;
            currCommit.CommiterId = userContext.UserId;
            currCommit.IsIndex = false;
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

        public IEnumerable<PartVersion> DeletedParts(int? commitId = null)
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

            return updatedPartVersion;
        }

        private PartVersion DeletePartVersion(PartVersion partVersion, UserContext userContext)
        {
            Commit currCommit = this.GetCommit();

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
