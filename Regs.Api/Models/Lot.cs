using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Api.UserContext;
using Common.Data;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;

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

            if (index.CommitVersions.Any(pc => pc.PartVersion.Part.Path == expandedPath))
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
                Content = json
            };

            CommitVersion commitVersion = new CommitVersion
            {
                PartVersion = partVersion,
                Commit = index
            };

            index.CommitVersions.Add(commitVersion);

            return partVersion;
        }

        public PartVersion UpdatePart(string path, JObject json, UserContext userContext)
        {
            PartVersion partVersion = this.GetPartVersions(path, true).Single();

            return this.UpdatePartVersion(partVersion, json, userContext);
        }

        public PartVersion DeletePart(string path, UserContext userContext, bool lastPartOnly = false)
        {
            if (lastPartOnly)
            {
                var allPartsPath = Regex.Match(path, @".+/\d+/[^/\d]+").Value;

                var lastPartVersion = this.GetPartVersions(allPartsPath, false)
                    .OrderByDescending(pv => pv.Part.Index)
                    .First();

                if (lastPartVersion.Part.Path != path)
                {
                    throw new Exception("Cannot delete part that is not last!");
                }
            }

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

            PartVersion partVersion = index.CommitVersions
                .Select(cv => cv.PartVersion)
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

            CommitVersion prevCommitVersion = index.ParentCommit.CommitVersions.FirstOrDefault(pv => pv.PartVersion.Part.Path == path && pv.PartVersion.PartOperation != PartOperation.Delete);
            if (prevCommitVersion != null)
            {
                CommitVersion newCommitVersion = new CommitVersion
                {
                    Commit = index,
                    PartVersion = prevCommitVersion.PartVersion,
                    OldPartVersion = prevCommitVersion.PartVersion
                };

                index.CommitVersions.Add(newCommitVersion);
            }

            return partVersion;
        }

        public void Reset(int commitId, UserContext userContext, ILotEventDispatcher lotEventDispatcher)
        {
            Commit index = this.Index;
            this.ModifyDate = DateTime.Now;

            bool commitVersionsInIndex = index.CommitVersions
                .Any(pv =>
                    pv.PartVersion.PartOperation != PartOperation.Delete &&
                    pv.PartVersion.OriginalCommit == index);

            if (commitVersionsInIndex)
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

                IEnumerable<PartVersion> partVersionsToDelete = newLastCommit.CommitVersions
                    .Select(cv => cv.PartVersion)
                    .Where(pv => pv.OriginalCommit == newLastCommit);

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
            }
            while (newLastCommit != null && newLastCommit.CommitId != commitId);

            if (newLastCommit == null)
            {
                throw new Exception(string.Format("No commit with id {0} found.", commitId));
            }

            Commit newIndex = new Commit
            {
                ParentCommit = newLastCommit,
                CommiterId = userContext.UserId,
                CommitDate = DateTime.Now,
                IsIndex = true,
                IsLoaded = true
            };
            newIndex.CommitVersions = newLastCommit.CommitVersions
                .Select(cv =>
                    new CommitVersion
                    {
                        Commit = newIndex,
                        PartVersion = cv.PartVersion,
                        OldPartVersion = cv.OldPartVersion
                    })
                .ToList();

            this.Commits.Add(newIndex);

            lotEventDispatcher.Dispatch(new ResetEvent(this, newIndex));
        }

        public void Commit(UserContext userContext, ILotEventDispatcher lotEventDispatcher, string[] paths = null)
        {
            Commit index = this.Index;

            List<CommitVersion> changedCommitVersions = index.CommitVersions.Where(pv => pv.PartVersion.OriginalCommit == index).ToList();
            List<CommitVersion> toBeCommited;
            List<CommitVersion> notToBeCommited;

            index.ChangedPartVersions = changedCommitVersions.Select(cv => cv.PartVersion).ToList();

            if (paths != null)
            {
                toBeCommited = changedCommitVersions.Where(cv => paths.Contains(cv.PartVersion.Part.Path)).ToList();
                notToBeCommited = changedCommitVersions.Except(toBeCommited).ToList();

                foreach (var commitVersion in notToBeCommited)
                {
                    index.CommitVersions.Remove(commitVersion);
                }
            }
            else
            {
                toBeCommited = changedCommitVersions;
                notToBeCommited = new List<CommitVersion>();
            }

            if (toBeCommited.Count == 0)
            {
                throw new InvalidOperationException("Cannot commit without modifications (empty commit)");
            }

            //update the date to force EF update
            this.ModifyDate = DateTime.Now;

            Commit newIndex = new Commit
            {
                ParentCommit = index,
                CommiterId = userContext.UserId,
                CommitDate = DateTime.Now
            };

            newIndex.IsIndex = true;
            newIndex.IsLoaded = true;
            newIndex.CommitVersions = notToBeCommited
                .Union(index.CommitVersions.Where(cv => cv.PartVersion.PartOperation != PartOperation.Delete))
                .Select(cv =>
                    new CommitVersion
                    {
                        Commit = newIndex,
                        PartVersion = cv.PartVersion,
                        OldPartVersion = cv.OldPartVersion
                    })
                .ToList();
            this.Commits.Add(newIndex);

            index.CommitDate = DateTime.Now;
            index.CommiterId = userContext.UserId;
            index.IsIndex = false;

            lotEventDispatcher.Dispatch(new CommitEvent(this, newIndex, index));
        }

        public PartVersion GetPart(string path, int? commitId = null)
        {
            return this.GetPartVersions(path, true, commitId)
                .FirstOrDefault();
        }

        public JObject GetPartContent(string path, int? commitId = null)
        {
            return this.GetPartVersions(path, true, commitId)
                .Select(pv => pv.Content)
                .FirstOrDefault();
        }

        public PartVersion[] GetParts(string path, int? commitId = null)
        {
            return this.GetPartVersions(path, false, commitId)
                .OrderBy(pv => pv.Part.Path)
                .ToArray();
        }

        public JObject[] GetPartsContent(string path, int? commitId = null)
        {
            return this.GetPartVersions(path, false, commitId)
                .OrderBy(pv => pv.Part.Path)
                .Select(pv => pv.Content)
                .ToArray();
        }

        public IEnumerable<PartVersion> GetPartVersions(string path, bool exact, int? commitId = null)
        {
            Commit commit = this.GetCommit(commitId);
            var partVersions =
                commit.CommitVersions
                .Select(cv => cv.PartVersion)
                .Where(pv =>
                    (exact ? pv.Part.Path == path : Regex.IsMatch(pv.Part.Path, "^" + path + @"/\d+$")) &&
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
                partVersion.Content = json;
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
                Content = json
            };

            var commitVersion = currCommit.CommitVersions.SingleOrDefault(cv => cv.PartVersion.Part == partVersion.Part);
            currCommit.CommitVersions.Remove(commitVersion);

            CommitVersion newCommitVersion = new CommitVersion
            {
                Commit = currCommit,
                PartVersion = updatedPartVersion,
                OldPartVersion = partVersion
            };
            currCommit.CommitVersions.Add(newCommitVersion);

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
                Part = partVersion.Part,
                Content = partVersion.Content
            };

            var commitVersion = currCommit.CommitVersions.SingleOrDefault(cv => cv.PartVersion.Part == partVersion.Part);
            currCommit.CommitVersions.Remove(commitVersion);

            CommitVersion newCommitVersion = new CommitVersion
            {
                Commit = currCommit,
                PartVersion = deletedPartVersion,
                OldPartVersion = partVersion
            };
            currCommit.CommitVersions.Add(newCommitVersion);

            return deletedPartVersion;
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
