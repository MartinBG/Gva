using System;
namespace Gva.Api.ModelsDO
{
    public class OrganizationDO
    {
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