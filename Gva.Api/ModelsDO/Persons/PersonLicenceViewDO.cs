using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceViewDO
    {
        public int PartIndex { get; set; }

        public NomValue LicenceType { get; set; }

        public int? LicenceNumber { get; set; }

        public string ForeignLicenceNumber { get; set; }

        public NomValue ForeignPublisher{ get; set; }

        public NomValue Employment { get; set; }

        public NomValue Publisher { get; set; }

        public NomValue Valid { get; set; }

        public List<PersonLicenceStatusViewDO> Statuses { get; set; }

        public int CaseTypeId { get; set; }
    }
}
