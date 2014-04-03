using System;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers
{
    public class OrganizationLotEventHandler : ILotEventHandler
    {
        private static string[] parts = { "data" };

        private IOrganizationRepository organizationRepository;

        public OrganizationLotEventHandler(IOrganizationRepository organizationRepository)
        {
            this.organizationRepository = organizationRepository;
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

            if (lot.Set.Alias != "Organization" ||
                !commit.ChangedPartVersions.Any(pv => parts.Contains(pv.Part.SetPart.Alias)))
            {
                return;
            }

            var organization = this.organizationRepository.GetOrganization(lot.LotId);

            if (organization == null)
            {
                organization = new GvaViewOrganization()
                {
                    Lot = lot
                };
                this.UpdateOrganizationData(organization, commit);
                this.organizationRepository.AddOrganization(organization);
            }
            else
            {
                this.UpdateOrganizationData(organization, commit);
            }
        }

        private void UpdateOrganizationData(GvaViewOrganization organization, Commit commit)
        {
            var organizationDataPart = commit.ChangedPartVersions
                .SingleOrDefault(pv => pv.Part.SetPart.Alias == "data");

            if (organizationDataPart != null)
            {
                dynamic organizationDataContent = organizationDataPart.Content;


                organization.Name = organizationDataContent.name;
                organization.CAO = organizationDataContent.CAO;
                organization.Valid = organizationDataContent.valid.name;
                organization.Uin = organizationDataContent.uin;
                organization.OrganizationType = organizationDataContent.organizationType.name;
                organization.DateValidTo = Convert.ToDateTime(organizationDataContent.dateValidTo);
                organization.DateCAOValidTo = Convert.ToDateTime(organizationDataContent.dateCAOValidTo);
            }
        }
    }
}