using Gva.Api.Models;
using System;
namespace Gva.Api.ModelsDO
{
    public class OrganizationDO
    {

        public OrganizationDO(GvaViewOrganization organizationData)
        {
            this.Id = organizationData.LotId;
            this.Name = organizationData.Name;
            this.CAO = organizationData.CAO;
            this.Uin = organizationData.Uin;
            this.Valid = organizationData.Valid;
            this.OrganizationType = organizationData.OrganizationType;
            this.DateValidTo = organizationData.DateValidTo;
            this.DateCAOValidTo = organizationData.DateCAOValidTo;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string CAO { get; set; }

        public string Uin { get; set; }

        public string Valid { get; set; }

        public string OrganizationType { get; set; }

        public DateTime? DateValidTo { get; set; }

        public DateTime? DateCAOValidTo { get; set; }

    }
}