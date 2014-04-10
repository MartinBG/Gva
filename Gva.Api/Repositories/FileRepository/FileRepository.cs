using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Data;
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
                        .Include(f => f.GvaAppLotFiles)
                        .Include(f => f.GvaFile)
                        .Include(f => f.DocFile)
                        .Include(f => f.GvaFile.GvaLotFiles)
                        .SingleOrDefault(f => f.GvaLotFileId == lotFileId);

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
                .Include(f => f.GvaFile)
                .Where(f => f.LotPartId == partVersion.Part.PartId);

            foreach (var lotFile in lotFiles)
            {
                this.DeleteLotFile(lotFile);
            }
        }

        public GvaLotFile[] GetFileReferences(int partId, int? caseType)
        {
            return this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.DocFile)
                .Include(f => f.GvaFile)
                .Include(f => f.GvaCaseType)
                .Include(f => f.GvaAppLotFiles)
                .Include(f => f.GvaAppLotFiles.Select(gf => gf.GvaApplication))
                .Where(f => f.LotPartId == partId && (caseType.HasValue ? f.GvaCaseTypeId == caseType : true))
                .OrderBy(f => f.FormPageIndex)
                .ToArray();
        }

        public GvaLotFile[] GetFileReferencesForLot(int lotId, int caseType)
        {
            return this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.GvaCaseType)
                .Include(f => f.DocFile)
                .Include(f => f.GvaFile)
                .Where(f => f.LotPart.LotId == lotId && f.GvaCaseTypeId == caseType)
                .OrderBy(f => f.FormPageIndex)
                .ToArray();
        }

        private GvaLotFile AddLotFile(Part part, FileDO fileObj)
        {
            var file = new GvaFile()
            {
                Filename = (string)fileObj.File.Name,
                FileContentId = fileObj.File.Key
            };

            this.unitOfWork.DbContext.Set<GvaFile>().Add(file);

            GvaLotFile newLotFile = new GvaLotFile()
            {
                LotPart = part,
                GvaFile = file,
                GvaCaseTypeId = fileObj.CaseType.NomValueId,
                PageNumber = (int?)fileObj.PageCount
            };
            newLotFile.SavePageIndex((string)fileObj.BookPageNumber);

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
            lotFile.PageNumber = fileObj.PageCount;
            lotFile.SavePageIndex((string)fileObj.BookPageNumber);

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
            if (lotFile.GvaFile == null)
            {
                throw new Exception(string.Format("Connot delete GvaLotFile with id: {0}", lotFile.GvaLotFileId));
            }

            foreach (var gvaAppLotFile in lotFile.GvaAppLotFiles.ToList())
            {
                this.unitOfWork.DbContext.Set<GvaAppLotFile>().Remove(gvaAppLotFile);
            }

            this.unitOfWork.DbContext.Set<GvaFile>().Remove(lotFile.GvaFile);
            this.unitOfWork.DbContext.Set<GvaLotFile>().Remove(lotFile);
        }
    }
}
