using System;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
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

        public void AddFileReferences(Part part, dynamic files)
        {
            if (files == null)
            {
                return;
            }

            foreach (var fileObj in files)
            {
                if ((bool)fileObj.isAdded)
                {
                    this.AddLotFile(part, fileObj);
                    continue;
                }

                var lotFileId = (int)fileObj.lotFileId;
                var lotFile = this.unitOfWork.DbContext.Set<GvaLotFile>()
                        .Include(f => f.GvaAppLotFiles)
                        .Include(f => f.GvaFile)
                        .Include(f => f.GvaFile.GvaLotFiles)
                        .SingleOrDefault(f => f.GvaLotFileId == lotFileId);

                if ((bool)fileObj.isDeleted)
                {
                    this.DeleteLotFile(lotFile);
                    continue;
                }

                this.UpdateLotFile(lotFile, fileObj);
            }
        }

        public void DeleteFileReferences(int partId)
        {
            var lotFiles = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.GvaFile)
                .Where(f => f.LotPartId == partId);

            foreach (var lotFile in lotFiles)
            {
                this.DeleteLotFile(lotFile);
            }
        }

        public GvaLotFile[] GetFileReferences(int partId)
        {
            return this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.DocFile)
                .Include(f => f.GvaFile)
                .Include(f => f.GvaCaseType)
                .Include(f => f.GvaAppLotFiles)
                .Include(f => f.GvaAppLotFiles.Select(gf => gf.GvaApplication))
                .Where(f => f.LotPartId == partId)
                .ToArray();
        }

        private GvaLotFile AddLotFile(Part part, dynamic fileObj)
        {
            var key = new Guid((string)fileObj.file.key);

            var file = new GvaFile()
            {
                Filename = (string)fileObj.file.name,
                FileContentId = key
            };

            this.unitOfWork.DbContext.Set<GvaFile>().Add(file);

            GvaLotFile newLotFile = new GvaLotFile()
            {
                LotPart = part,
                GvaFile = file,
                GvaCaseTypeId = fileObj.caseType.nomValueId,
                PageNumber = (int?)fileObj.pageCount,
                PageIndex = fileObj.bookPageNumber
            };

            this.unitOfWork.DbContext.Set<GvaLotFile>().Add(newLotFile);

            foreach (var application in fileObj.applications)
            {
                GvaAppLotFile appLotFile = new GvaAppLotFile()
                {
                    GvaApplicationId = application.applicationId,
                    GvaLotFile = newLotFile
                };

                this.unitOfWork.DbContext.Set<GvaAppLotFile>().Add(appLotFile);
            }

            return newLotFile;
        }

        private void UpdateLotFile(GvaLotFile lotFile, dynamic fileObj)
        {
            lotFile.GvaCaseTypeId = fileObj.caseType.nomValueId;
            lotFile.PageNumber = fileObj.pageCount;
            lotFile.PageIndex = fileObj.bookPageNumber;

            var nonModifiedApps = lotFile.GvaAppLotFiles.Join(
                (JArray)fileObj.applications,
                gf => gf.GvaApplicationId,
                a => a.Value<int>("applicationId"),
                (gf, a) => gf);

            var removedApplications = lotFile.GvaAppLotFiles.Except(nonModifiedApps).ToList();
            foreach (var application in removedApplications)
            {
                this.unitOfWork.DbContext.Set<GvaAppLotFile>().Remove(application);
            }

            foreach (var application in fileObj.applications)
            {
                var appLotFile = lotFile.GvaAppLotFiles.SingleOrDefault(af => af.GvaApplicationId == (int)application.applicationId);
                if (appLotFile == null)
                {
                    appLotFile = new GvaAppLotFile()
                    {
                        GvaApplicationId = application.applicationId,
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
