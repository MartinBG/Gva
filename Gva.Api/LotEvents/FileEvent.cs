using System.Collections.Generic;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.LotEvents
{
    public class FileEvent : IEvent
    {
        public FileEvent(List<GvaLotFile> addedFiles, List<GvaLotFile> updatedFiles, List<GvaLotFile> deletedFiles, PartVersion partVersion)
        {
            this.AddedFiles = addedFiles;
            this.UpdatedFiles = updatedFiles;
            this.DeletedFiles = deletedFiles;
            this.PartVersion = partVersion;
        }

        public List<GvaLotFile> AddedFiles { get; set; }

        public List<GvaLotFile> UpdatedFiles { get; set; }

        public List<GvaLotFile> DeletedFiles { get; set; }

        public PartVersion PartVersion { get; set; }
    }
}
