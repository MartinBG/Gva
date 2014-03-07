using System;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
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

        public void AddFileReference(Part part, Guid key, string name)
        {
            var file = this.unitOfWork.DbContext.Set<GvaFile>()
                .SingleOrDefault(f => f.FileContentId == key);

            if (file == null)
            {
                file = new GvaFile()
                {
                    Filename = name,
                    FileContentId = key
                };

                this.unitOfWork.DbContext.Set<GvaFile>().Add(file);
            }

            GvaLotFile lotFile = new GvaLotFile()
            {
                LotPart = part,
                GvaFile = file
            };

            this.unitOfWork.DbContext.Set<GvaLotFile>().Add(lotFile);
        }

        public void GetFileReference(int partId)
        {
            var lotFile = this.unitOfWork.DbContext.Set<GvaLotFile>()
                .FirstOrDefault(f => f.LotPartId == partId);

            if (lotFile == null)
            {
                //return null;
            }

            if (lotFile.DocFileId.HasValue)
            {

            }
        }
    }
}
