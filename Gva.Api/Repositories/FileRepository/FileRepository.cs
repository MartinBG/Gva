using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Data;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Newtonsoft.Json.Linq;
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

        public void AddFileReferences(PartVersion partVersion, IEnumerable<FileDO> files)
        {
            if (files == null)
            {
                return;
            }

            foreach (var fileObj in files)
            {
                if ((bool)fileObj.IsAdded)
                {
                    var newFile = this.AddLotFile(partVersion.Part, fileObj);
                    continue;
                }

                var lotFileId = (int)fileObj.LotFileId;
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

                if ((bool)fileObj.IsDeleted)
                {
                    this.DeleteLotFile(lotFile);
                    continue;
                }

                this.UpdateLotFile(lotFile, fileObj);
            }
        }

        public void DeleteFileReferences(PartVersion partVersion)
        {
            var lotFiles = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.GvaAppLotFiles)
                .Include(f => f.GvaFile)
                .Include(f => f.DocFile)
                .Where(f => f.LotPartId == partVersion.Part.PartId);

            foreach (var lotFile in lotFiles)
            {
                this.DeleteLotFile(lotFile);
            }
        }

        public GvaLotFile[] GetFileReferences(int partId, int? caseType)
        {
            var result = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.DocFile)
                .Include(f => f.GvaFile)
                .Include(f => f.GvaCaseType)
                .Where(f => f.LotPartId == partId && (caseType.HasValue ? f.GvaCaseTypeId == caseType : true))
                .ToArray();

            this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                .Include(ga => ga.GvaApplication)
                .Where(ga => ga.GvaLotFile.LotPartId == partId && (caseType.HasValue ? ga.GvaLotFile.GvaCaseTypeId == caseType : true))
                .Load();

            return result;
        }

        private GvaLotFile AddLotFile(Part part, FileDO fileObj)
        {
            GvaFile file = null;
            if (fileObj.File != null)
            {
                file = new GvaFile()
                {
                    Filename = fileObj.File.Name,
                    MimeType = fileObj.File.MimeType,
                    FileContentId = fileObj.File.Key
                };

                this.unitOfWork.DbContext.Set<GvaFile>().Add(file);
            }

            GvaLotFile newLotFile = new GvaLotFile()
            {
                LotPart = part,
                GvaFile = file,
                GvaCaseTypeId = fileObj.CaseType.NomValueId,
                PageIndex = (string)fileObj.BookPageNumber,
                PageNumber = (int?)fileObj.PageCount
            };

            this.unitOfWork.DbContext.Set<GvaLotFile>().Add(newLotFile);

            foreach (var application in fileObj.Applications)
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

        private void UpdateLotFile(GvaLotFile lotFile, FileDO fileObj)
        {
            lotFile.GvaCaseTypeId = fileObj.CaseType.NomValueId;
            lotFile.PageIndex = fileObj.BookPageNumber;
            lotFile.PageNumber = fileObj.PageCount;

            var nonModifiedApps = lotFile.GvaAppLotFiles.Join(
                fileObj.Applications,
                gf => gf.GvaApplicationId,
                a => a.ApplicationId,
                (gf, a) => gf);

            var removedApplications = lotFile.GvaAppLotFiles.Except(nonModifiedApps).ToList();
            foreach (var application in removedApplications)
            {
                this.unitOfWork.DbContext.Set<GvaAppLotFile>().Remove(application);
            }

            foreach (var application in fileObj.Applications)
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
    }
}
