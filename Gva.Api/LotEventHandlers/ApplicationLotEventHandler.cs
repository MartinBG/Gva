using System.Linq;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Gva.Api.Repositories.ApplicationRepository;

namespace Gva.Api.LotEventHandlers
{
    public class ApplicationLotEventHandler : ILotEventHandler
    {
        private IApplicationRepository applicationRepository;

        public ApplicationLotEventHandler(IApplicationRepository applicationRepository)
        {
            this.applicationRepository = applicationRepository;
        }

        public void Handle(ILotEvent e)
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
                    dynamic applicationPartContent = applicationPart.Content;

                    GvaApplicationSearch gvaApplicationSearch = new GvaApplicationSearch()
                    {
                        RequestDate = applicationPartContent.requestDate,
                        DocumentNumber = applicationPartContent.documentNumber,
                        ApplicationTypeName = applicationPartContent.applicationType != null ? applicationPartContent.applicationType.name : null,
                        //StatusName = applicationPartContent.applicationStatus != null ? applicationPartContent.applicationStatus.name : null
                    };

                    this.applicationRepository.AddGvaApplicationSearch(gvaApplicationSearch);
                }

                if (applicationPart.PartOperation == PartOperation.Delete)
                {
                    this.applicationRepository.DeleteGvaApplicationSearch(applicationPart.Part.PartId);
                }

                if (applicationPart.PartOperation == PartOperation.Update)
                {
                    dynamic applicationPartContent = applicationPart.Content;

                    var gvaApplicationSearch = this.applicationRepository.GetGvaApplicationSearch(applicationPart.Part.PartId);

                    if (gvaApplicationSearch != null)
                    {
                        gvaApplicationSearch.RequestDate = applicationPartContent.requestDate;
                        gvaApplicationSearch.DocumentNumber = applicationPartContent.documentNumber;
                        gvaApplicationSearch.ApplicationTypeName = applicationPartContent.applicationType != null ? applicationPartContent.applicationType.name : null;
                    }
                }
            }
        }
    }
}
