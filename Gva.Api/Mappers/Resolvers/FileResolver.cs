using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Common.Api.Models;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Mappers.Resolvers
{
    public class FileResolver : ValueResolver<Tuple<int, int, int?>, FileDO[]>
    {
        private IFileRepository fileRepository;
        private ILotRepository lotRepository;

        public FileResolver(IFileRepository fileRepository, ILotRepository lotRepository)
        {
            this.fileRepository = fileRepository;
            this.lotRepository = lotRepository;
        }

        protected override FileDO[] ResolveCore(Tuple<int, int, int?> tuple)
        {
            this.lotRepository.GetLotIndex(tuple.Item1);

            var lotFiles = this.fileRepository.GetFileReferences(tuple.Item2, tuple.Item3);

            var files = new List<FileDO>();
            foreach (var lotFile in lotFiles)
            {
                files.Add(this.GetFileDO(lotFile));
            }

            return files.ToArray();
        }

        private FileDO GetFileDO(GvaLotFile lotFile)
        {
            FileDO file = new FileDO()
            {
                LotFileId = lotFile.GvaLotFileId,
                BookPageNumber = lotFile.PageIndex,
                PageCount = lotFile.PageNumber,
                CaseType = Mapper.Map<GvaCaseType, NomValue>(lotFile.GvaCaseType),
                Applications = Mapper.Map<List<GvaApplication>, List<ApplicationNomDO>>(lotFile.GvaAppLotFiles.Select(af => af.GvaApplication).ToList())
            };

            FileDataDO fileData = new FileDataDO();
            if (lotFile.DocFileId.HasValue)
            {
                fileData.Name = lotFile.DocFile.DocFileName;
                fileData.Key = lotFile.DocFile.DocFileContentId;
                file.IsDocFile = true;
            }
            else
            {
                fileData.Name = lotFile.GvaFile.Filename;
                fileData.Key = lotFile.GvaFile.FileContentId;
                file.IsDocFile = false;
            }

            file.File = fileData;

            return file;
        }
    }
}
