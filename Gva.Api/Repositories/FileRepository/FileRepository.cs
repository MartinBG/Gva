using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Data;
using Common.Linq;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

namespace Gva.Api.Repositories.FileRepository
{
    public class FileRepository : IFileRepository
    {
        private IUnitOfWork unitOfWork;

        public FileRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddFileReferences(Part part, IEnumerable<CaseDO> cases)
        {
            if (cases == null)
            {
                return;
            }

            foreach (var caseDO in cases)
            {
                if (caseDO.IsAdded)
                {
                    this.AddLotFile(part, caseDO);
                    continue;
                }

                var lotFileId = caseDO.LotFileId;
                var lotFile = this.unitOfWork.DbContext.Set<GvaLotFile>()
                        .Include(f => f.DocFile)
                        .SingleOrDefault(f => f.GvaLotFileId == lotFileId);

                this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                    .Where(ga => ga.GvaLotFileId == lotFileId)
                    .Load();

                this.unitOfWork.DbContext.Set<GvaFile>()
                    .Include(f => f.GvaLotFiles)
                    .Where(f => f.GvaFileId == lotFile.GvaFileId)
                    .Load();

                if (caseDO.IsDeleted)
                {
                    this.DeleteLotFile(lotFile);
                    continue;
                }

                this.UpdateLotFile(lotFile, caseDO);
            }
        }

        public void AddFileReference(Part part, CaseDO caseDO)
        {
            if (caseDO == null)
            {
                return;
            }

            if (caseDO.IsAdded)
            {
                var oldFile = this.unitOfWork.DbContext.Set<GvaLotFile>()
                    .SingleOrDefault(f => f.LotPartId == part.PartId);
                if (oldFile != null)
                {
                    this.DeleteLotFile(oldFile);
                }

                var newFile = this.AddLotFile(part, caseDO);
            }
            else
            {
                var lotFileId = caseDO.LotFileId;
                var lotFile = this.unitOfWork.DbContext.Set<GvaLotFile>()
                        .Include(f => f.DocFile)
                        .SingleOrDefault(f => f.GvaLotFileId == lotFileId);

                this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                    .Where(ga => ga.GvaLotFileId == lotFileId)
                    .Load();

                this.unitOfWork.DbContext.Set<GvaFile>()
                    .Include(f => f.GvaLotFiles)
                    .Where(f => f.GvaFileId == lotFile.GvaFileId)
                    .Load();

                if (caseDO.IsDeleted)
                {
                    this.DeleteLotFile(lotFile);
                }
                else
                {
                    this.UpdateLotFile(lotFile, caseDO);
                }
            }
        }

        public void DeleteFileReferences(Part part)
        {
            var lotFiles = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.GvaAppLotFiles)
                .Include(f => f.GvaFile)
                .Include(f => f.DocFile)
                .Where(f => f.LotPartId == part.PartId);

            foreach (var lotFile in lotFiles)
            {
                this.DeleteLotFile(lotFile);
            }
        }

        public GvaLotFile[] GetFileReferences(int partId, int? caseType)
        {
            var predicate = PredicateBuilder.True<GvaLotFile>()
                .And(f => f.LotPartId == partId);

            if (caseType.HasValue)
            {
                predicate = predicate.And(f => f.GvaCaseTypeId == caseType);
            }

            var result = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.DocFile)
                .Include(f => f.GvaFile)
                .Include(f => f.GvaCaseType)
                .Where(predicate)
                .ToArray();

            this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                .Include(ga => ga.GvaApplication)
                .Where(ga => ga.GvaLotFile.LotPartId == partId && (caseType.HasValue ? ga.GvaLotFile.GvaCaseTypeId == caseType : true))
                .Load();

            return result;
        }

        public GvaLotFile GetFileReference(int partId, int? caseType)
        {
            var predicate = PredicateBuilder.True<GvaLotFile>()
                .And(f => f.LotPartId == partId);

            if (caseType.HasValue)
            {
                predicate = predicate.And(f => f.GvaCaseTypeId == caseType);
            }

            var result = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.DocFile)
                .Include(f => f.GvaFile)
                .Include(f => f.GvaCaseType)
                .Where(predicate)
                .ToList();

            this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                .Include(ga => ga.GvaApplication)
                .Where(ga => ga.GvaLotFile.LotPartId == partId && (caseType.HasValue ? ga.GvaLotFile.GvaCaseTypeId == caseType : true))
                .Load();

            return result.Count == 0 ? null : result.Single();
        }

        private GvaLotFile AddLotFile(Part part, CaseDO caseDO)
        {
            GvaFile file = null;
            if (caseDO.File != null)
            {
                file = new GvaFile()
                {
                    Filename = caseDO.File.Name,
                    MimeType = caseDO.File.MimeType,
                    FileContentId = caseDO.File.Key
                };

                this.unitOfWork.DbContext.Set<GvaFile>().Add(file);
            }

            GvaLotFile newLotFile = new GvaLotFile()
            {
                LotPart = part,
                GvaFile = file,
                GvaCaseTypeId = caseDO.CaseType.NomValueId,
                PageIndex = (string)caseDO.BookPageNumber,
                PageIndexInt = GetPageIndexInt(caseDO.BookPageNumber),
                PageNumber = (int?)caseDO.PageCount,
                Note = caseDO.Note,
            };

            this.unitOfWork.DbContext.Set<GvaLotFile>().Add(newLotFile);

            foreach (var application in caseDO.Applications)
            {
                GvaAppLotFile appLotFile = new GvaAppLotFile()
                {
                    GvaApplicationId = application.ApplicationId,
                    GvaLotFile = newLotFile
                };

                this.unitOfWork.DbContext.Set<GvaAppLotFile>().Add(appLotFile);
            }

            return newLotFile;
        }

        private void UpdateLotFile(GvaLotFile lotFile, CaseDO caseDO)
        {
            lotFile.GvaCaseTypeId = caseDO.CaseType.NomValueId;
            lotFile.PageIndex = caseDO.BookPageNumber;
            lotFile.PageIndexInt = GetPageIndexInt(caseDO.BookPageNumber);
            lotFile.PageNumber = caseDO.PageCount;
            lotFile.Note = caseDO.Note;

            if (caseDO.File != null)
            {
                if (!lotFile.GvaFileId.HasValue)
                {
                    GvaFile file = new GvaFile()
                    {
                        Filename = caseDO.File.Name,
                        MimeType = caseDO.File.MimeType,
                        FileContentId = caseDO.File.Key
                    };

                    this.unitOfWork.DbContext.Set<GvaFile>().Add(file);
                    lotFile.GvaFile = file;
                }
                else if (lotFile.GvaFile.FileContentId != caseDO.File.Key)
                {
                    lotFile.GvaFile.Filename = caseDO.File.Name;
                    lotFile.GvaFile.MimeType = caseDO.File.MimeType;
                    lotFile.GvaFile.FileContentId = caseDO.File.Key;
                }
            }
            else if (lotFile.GvaFileId.HasValue)
            {
                this.unitOfWork.DbContext.Set<GvaFile>().Remove(lotFile.GvaFile);
            }

            var nonModifiedApps = lotFile.GvaAppLotFiles.Join(
                caseDO.Applications,
                gf => gf.GvaApplicationId,
                a => a.ApplicationId,
                (gf, a) => gf);

            var removedApplications = lotFile.GvaAppLotFiles.Except(nonModifiedApps).ToList();
            foreach (var application in removedApplications)
            {
                this.unitOfWork.DbContext.Set<GvaAppLotFile>().Remove(application);
            }

            foreach (var application in caseDO.Applications)
            {
                var appLotFile = lotFile.GvaAppLotFiles.SingleOrDefault(af => af.GvaApplicationId == (int)application.ApplicationId);
                if (appLotFile == null)
                {
                    appLotFile = new GvaAppLotFile()
                    {
                        GvaApplicationId = application.ApplicationId,
                        GvaLotFile = lotFile
                    };

                    this.unitOfWork.DbContext.Set<GvaAppLotFile>().Add(appLotFile);
                }
            }
        }

        private void DeleteLotFile(GvaLotFile lotFile)
        {
            foreach (var gvaAppLotFile in lotFile.GvaAppLotFiles.ToList())
            {
                this.unitOfWork.DbContext.Set<GvaAppLotFile>().Remove(gvaAppLotFile);
            }

            if (lotFile.GvaFile != null)
            {
                this.unitOfWork.DbContext.Set<GvaFile>().Remove(lotFile.GvaFile);
            }
            if (lotFile.DocFile != null)
            {
                this.unitOfWork.DbContext.Set<DocFile>().Remove(lotFile.DocFile);
            }
            
            this.unitOfWork.DbContext.Set<GvaLotFile>().Remove(lotFile);
        }

        public bool IsUniqueBPN(int lotId, int caseTypeId, string bookPageNumber, int? fileId = null)
        {
            var lotFiles = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Where(p => p.LotPart.LotId == lotId && p.GvaCaseTypeId == caseTypeId && p.PageIndex == bookPageNumber);

            if (fileId.HasValue)
            {
                return !lotFiles.Where(f => f.GvaLotFileId != fileId).Any();
            }
            else
            {
                return !lotFiles.Any();
            }
        }

        public int? GetPageIndexInt(string pageIndex)
        {
            Regex regExp = new Regex(@"^(\d+)");
            if (pageIndex != null && regExp.IsMatch(pageIndex))
            {
                return Int32.Parse(regExp.Match(pageIndex).Value);
            }
            else
            {
                return null;
            }
        }
    }
}
