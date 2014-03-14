using System.Linq;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers
{
    public class ApplicationLotEventHandler : ILotEventHandler
    {
        private IFileRepository fileRepository;

        public ApplicationLotEventHandler(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        public void Handle(IEvent e)
        {
            CommitEvent commitEvent = e as CommitEvent;
            if (commitEvent == null)
            {
                return;
            }

            var commit = commitEvent.Commit;
            var lot = commitEvent.Lot;

            var applicationPart = commit.ChangedPartVersions.SingleOrDefault(pv => pv.Part.SetPart.Alias == "application");
            if (applicationPart != null)
            {
                if (applicationPart.PartOperation == PartOperation.Add)
                {
                    GvaApplication application = new GvaApplication()
                    {
                        Lot = lot,
                        GvaAppLotPart = applicationPart.Part
                    };

                    this.fileRepository.AddApplication(application);
                }

                if (applicationPart.PartOperation == PartOperation.Delete)
                {
                    this.fileRepository.DeleteApplication(applicationPart.Part.PartId);
                }
            }
        }
    }
}
