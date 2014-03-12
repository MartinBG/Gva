using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
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

        public void AddFileReferences(Part part, dynamic files)
        {
            foreach (var fileObj in files)
            {
                var key = new Guid((string)fileObj.file.key);

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

                lotFile.PageNumber = fileObj.bookPageNumber;
                lotFile.PageIndex = fileObj.pageCount;
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

        public FileDO[] GetFileReferences(int partId)
        {
            var lotFiles = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .Include(f => f.DocFile)
                .Include(f => f.GvaFile)
                .Where(f => f.LotPartId == partId);

            var files = new List<FileDO>();
            foreach (var lotFile in lotFiles)
            {
                FileDataDO fileData = new FileDataDO();

                if (lotFile.DocFileId.HasValue)
                {
                    fileData.Name = lotFile.DocFile.DocFileName;
                    fileData.Key = lotFile.DocFile.DocFileContentId;
                }
                else
                {
                    fileData.Name = lotFile.GvaFile.Filename;
                    fileData.Key = lotFile.GvaFile.FileContentId;
                }

                FileDO file = new FileDO()
                {
                    LotFileId = lotFile.GvaLotFileId,
                    BookPageNumber = lotFile.PageIndex,
                    PageCount = lotFile.PageNumber,
                    File = fileData
                };

                files.Add(file);
            }

            return files.ToArray();
        }

        public ApplicationDO[] GetFileApplications(int partId)
        {
            var gvaApplications = this.unitOfWork.DbContext.Set<GvaAppLotFile>()
                .Include(af => af.GvaApplication)
                .Include(af => af.GvaApplication.Doc)
                .Include(af => af.GvaApplication.Doc.DocType)
                .Where(af => af.GvaLotFile.LotPartId == partId)
                .Select(af => af.GvaApplication);

            var applications = new List<ApplicationDO>();
            foreach (var app in gvaApplications)
            {
                ApplicationDO application = new ApplicationDO()
                {
                    ApplicationId = app.GvaApplicationId,
                    ApplicationName = app.Doc.DocType.Name,
                    RegIndex = app.Doc.RegIndex
                };

                applications.Add(application);
            }

            return applications.ToArray();
        }

        private void AddLotFile(Part part, dynamic fileObj)
        {
            var key = new Guid((string)fileObj.file.key);

            var file = this.unitOfWork.DbContext.Set<GvaFile>()
                .SingleOrDefault(f => f.FileContentId == key);

            if (file == null)
            {
                file = new GvaFile()
                {
                    Filename = (string)fileObj.file.name,
                    FileContentId = key
                };

                this.unitOfWork.DbContext.Set<GvaFile>().Add(file);
            }

            GvaLotFile newLotFile = new GvaLotFile()
            {
                LotPart = part,
                GvaFile = file,
                GvaCaseTypeId = 1, // TO DO
                PageNumber = fileObj.bookPageNumber,
                PageIndex = fileObj.pageCount
            };

            this.unitOfWork.DbContext.Set<GvaLotFile>().Add(newLotFile);
        }

        private void DeleteLotFile(GvaLotFile lotFile)
        {
            foreach (var gvaAppLotFile in lotFile.GvaAppLotFiles)
            {
                this.unitOfWork.DbContext.Set<GvaAppLotFile>().Remove(gvaAppLotFile);
            }

            var gvaFile = lotFile.GvaFile;
            this.unitOfWork.DbContext.Set<GvaLotFile>().Remove(lotFile);

            if (gvaFile != null && gvaFile.GvaLotFiles.Count == 0)
            {
                this.unitOfWork.DbContext.Set<GvaFile>().Remove(gvaFile);
            }
        }
    }
}
