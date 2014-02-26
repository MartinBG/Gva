using Common.Data;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Common.Api.UserContext;
using System.Text.RegularExpressions;

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
        public DateTime ModifyDate { get; set; }
        public byte[] Version { get; set; }

        public virtual ICollection<Commit> Commits { get; set; }
        public virtual ICollection<Part> Parts { get; set; }
        public virtual Set Set { get; set; }

        public Commit Index
        {
            get
            {
                return this.GetCommit();
            }
        }

        public Commit LastCommit
        {
            get
            {
                Commit commit = this.Commits.Where(c => c.IsIndex).Select(c => c.ParentCommit).Single();

                commit.EnsureIsLoaded();

                return commit;
            }
        }

        public PartVersion CreatePart(string path, JObject json, UserContext userContext)
        {
            Commit index = this.Index;
            this.ModifyDate = DateTime.Now;

            string expandedPath;
            int partIndex;
            if (path.EndsWith("/*"))
            {
                partIndex = this.NextIndex++;
                expandedPath = path.Replace("*", partIndex.ToString());
            }
            else
            {
                partIndex = 0;
                expandedPath = path;
            }

            if (index.PartVersions.Any(pv => pv.Part.Path == expandedPath))
            {
                throw new Exception(string.Format("Specified path ({0}) is already in index", expandedPath.ToString()));
            }

            Part part = this.Parts.FirstOrDefault(p => p.Path == expandedPath);
            if (part == null)
            {
                part = new Part
                {
                    SetPart = this.GetSetPart(expandedPath),
                    Lot = this,
                    Path = expandedPath,
                    Index = partIndex
                };
                this.Parts.Add(part);
            }

            PartVersion partVersion = new PartVersion
            {
                OriginalCommit = index,
                PartOperation = PartOperation.Add,
                CreatorId = userContext.UserId,
                CreateDate = DateTime.Now,
                Part = part,
                TextContent = json.ToString()
            };
            index.PartVersions.Add(partVersion);

            return partVersion;
        }

        public PartVersion UpdatePart(string path, JObject json, UserContext userContext)
        {
            PartVersion partVersion = this.GetPartVersions(path, true).Single();

            return this.UpdatePartVersion(partVersion, json, userContext);
        }

        public PartVersion DeletePart(string path, UserContext userContext)
        {
            PartVersion partVersion = this.GetPartVersions(path, true).Single();

            return this.DeletePartVersion(partVersion, userContext);
        }

        public PartVersion ResetPart(string path)
        {
            Commit index = this.Index;

            if (!index.ParentCommit.IsLoaded)
            {
                throw new InvalidOperationException("Cannot reset a part if the last commit is not loaded.");
            }

            this.ModifyDate = DateTime.Now;

            PartVersion partVersion = index.PartVersions
                .FirstOrDefault(pv => 
                    pv.Part.Path == path &&
                    pv.OriginalCommit == index);

            if (partVersion == null)
            {
                throw new InvalidOperationException("No changes to reset!");
            }

            Part part = partVersion.Part;

            part.PartVersions.Remove(partVersion);

            if (partVersion.PartOperation == PartOperation.Add)
            {
                this.Parts.Remove(part);
            }

            PartVersion prevPartVersion = index.ParentCommit.PartVersions.FirstOrDefault(pv => pv.Part.Path == path && pv.PartOperation != PartOperation.Delete);
            if (prevPartVersion != null)
            {
                index.PartVersions.Add(prevPartVersion);
            }

            return partVersion;
        }

        public void Reset(int commitId, UserContext userContext)
        {
            Commit index = this.Index;
            this.ModifyDate = DateTime.Now;

            bool partVersionsInIndex = index.PartVersions
                .Any(pv =>
                    pv.PartOperation != PartOperation.Delete &&
                    pv.OriginalCommit == index);

            if (partVersionsInIndex)
            {
                throw new InvalidOperationException("Cannot reset with uncommited changes in index");
            }

            Commit newLastCommit = this.LastCommit;

            this.Commits.Remove(index);

            do
            {
                if (!newLastCommit.IsLoaded)
                {
                    throw new InvalidOperationException("Cannot reset if any of the commits from the index to the new index is not loaded.");
                }

                IEnumerable<PartVersion> partVersionsToDelete = newLastCommit.PartVersions.Where(pv => pv.OriginalCommit == newLastCommit);

                foreach (PartVersion partVersion in partVersionsToDelete)
                {
                    Part part = partVersion.Part;
                    part.PartVersions.Remove(partVersion);

                    if (partVersion.PartOperation == PartOperation.Add)
                    {
                        this.Parts.Remove(part);
                    }
                }

                this.Commits.Remove(newLastCommit);

                newLastCommit = newLastCommit.ParentCommit;
            } while (newLastCommit != null && newLastCommit.CommitId != commitId);

            if (newLastCommit == null)
            {
                throw new Exception(string.Format("No commit with id {0} found.", commitId));
            }

            Commit newIndex = new Commit
            {
                PartVersions = newLastCommit.PartVersions,
                ParentCommit = newLastCommit,
                CommiterId = userContext.UserId,
                CommitDate = DateTime.Now,
                IsIndex = true,
                IsLoaded = true
            };

            this.Commits.Add(newIndex);

            Events.Raise(new ResetEvent(this, newIndex));
        }

        public void Commit(UserContext userContext, string[] paths = null)
        {
            Commit index = this.Index;
            this.ModifyDate = DateTime.Now;

            Commit newIndex = new Commit
            {
                ParentCommit = index,
                CommiterId = userContext.UserId,
                CommitDate = DateTime.Now
            };

            List<PartVersion> changedPartVersions = index.PartVersions.Where(pv => pv.OriginalCommit == index).ToList();
            List<PartVersion> toBeCommited;
            List<PartVersion> notToBeCommited;

            if (paths != null)
            {
                toBeCommited = changedPartVersions.Where(pv => paths.Contains(pv.Part.Path)).ToList();
                notToBeCommited = changedPartVersions.Except(toBeCommited).ToList();

                foreach (var partVersion in notToBeCommited)
                {
                    partVersion.OriginalCommit = newIndex;
                    index.PartVersions.Remove(partVersion);
                }
            }
            else
            {
                toBeCommited = changedPartVersions;
                notToBeCommited = new List<PartVersion>();
            }

            if (toBeCommited.Count == 0)
            {
                throw new InvalidOperationException("Cannot commit without modifications (empty commit)");
            }

            newIndex.IsIndex = true;
            newIndex.IsLoaded = true;
            newIndex.PartVersions = new Collection<PartVersion>(notToBeCommited.Union(index.PartVersions.Where(pv => pv.PartOperation != PartOperation.Delete)).ToList());
            this.Commits.Add(newIndex);

            index.CommitDate = DateTime.Now;
            index.CommiterId = userContext.UserId;
            index.IsIndex = false;

            Events.Raise(new CommitEvent(this, newIndex, index));
        }

        public PartVersion GetPart(string path, int? commitId = null)
        {
            return this.GetPartVersions(path, true, commitId)
                .FirstOrDefault();
        }

        public PartVersion[] GetParts(string path, int? commitId = null)
        {
            return this.GetPartVersions(path, false, commitId)
                .OrderBy(pv => pv.Part.Path)
                .ToArray();
        }

        public IEnumerable<PartVersion> GetAddedPartVersions(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Add }, commitId);
        }

        public IEnumerable<PartVersion> GetUpdatedPartVersions(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Update }, commitId);
        }

        public IEnumerable<PartVersion> GetDeletedParts(int? commitId = null)
        {
            return this.GetPartsByOperations(new PartOperation[] { PartOperation.Delete }, commitId);
        }

        public IEnumerable<PartVersion> GetPartVersions(string path, bool exact, int? commitId = null)
        {
            Commit commit = this.GetCommit(commitId);
            var partVersions =
                commit.PartVersions
                .Where(pv =>
                    (exact ? pv.Part.Path == path : pv.Part.Path.StartsWith(path)) &&
                    pv.PartOperation != PartOperation.Delete);

            if (exact)
            {
                if (partVersions.Count() > 1)
                {
                    throw new Exception(string.Format("More than one part with path: {0}", path));
                }
            }

            return partVersions.ToList();
        }

        private PartVersion UpdatePartVersion(PartVersion partVersion, JObject json, UserContext userContext)
        {
            Commit currCommit = this.Index;
            this.ModifyDate = DateTime.Now;

            if (partVersion.OriginalCommit == currCommit)
            {
                partVersion.TextContent = json.ToString();
                partVersion.CreatorId = userContext.UserId;
                partVersion.CreateDate = DateTime.Now;
                return partVersion;
            }

            PartVersion updatedPartVersion = new PartVersion
            {
                OriginalCommit = currCommit,
                PartOperation = PartOperation.Update,
                CreatorId = userContext.UserId,
                CreateDate = DateTime.Now,
                Part = partVersion.Part,
                TextContent = json.ToString()
            };
            currCommit.PartVersions.Remove(partVersion);
            currCommit.PartVersions.Add(updatedPartVersion);

            return updatedPartVersion;
        }

        private PartVersion DeletePartVersion(PartVersion partVersion, UserContext userContext)
        {
            Commit currCommit = this.Index;
            this.ModifyDate = DateTime.Now;

            if (partVersion.OriginalCommit == currCommit)
            {
                throw new InvalidOperationException("Cannot delete partVersion added in the same commit!");
            }

            PartVersion deletedPartVersion = new PartVersion
            {
                OriginalCommit = currCommit,
                PartOperation = PartOperation.Delete,
                CreatorId = userContext.UserId,
                CreateDate = DateTime.Now,
                Part = partVersion.Part
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
                this.Commits.SingleOrDefault(c => c.CommitId == commitId) :
                this.Commits.SingleOrDefault(c => c.IsIndex == true);

            if (commit == null)
            {
                if (commitId.HasValue)
                {
                    throw new Exception(string.Format("No commit with id {0} found.", commitId));
                }
                else
                {
                    throw new Exception("No index commit found.");
                }
            }

            commit.EnsureIsLoaded();

            return commit;
        }

        private SetPart GetSetPart(string path)
        {
            SetPart setPart = this.Set.SetParts.Where(sp => Regex.IsMatch(path, sp.PathRegex)).SingleOrDefault();
            if (setPart == null)
            {
                throw new Exception(string.Format("Cannot find matching SetPart for path: {0}", path));
            }

            return setPart;
        }
    }

    public class LotMap : EntityTypeConfiguration<Lot>
    {
        public LotMap()
        {
            // Primary Key
            this.HasKey(t => t.LotId);

            // Properties
            this.Property(t => t.Version)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8)
                .IsRowVersion();

            // Table & Column Mappings
            this.ToTable("Lots");
            this.Property(t => t.LotId).HasColumnName("LotId");
            this.Property(t => t.SetId).HasColumnName("LotSetId");
            this.Property(t => t.NextIndex).HasColumnName("NextIndex");
            this.Property(t => t.ModifyDate).HasColumnName("ModifyDate");
            this.Property(t => t.Version).HasColumnName("Version");

            // Relationships
            this.HasRequired(t => t.Set)
                .WithMany(t => t.Lots)
                .HasForeignKey(d => d.SetId);

            // Local-only properties
            this.Ignore(t => t.Index);
            this.Ignore(t => t.LastCommit);
        }
    }
}
