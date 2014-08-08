using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceDO
    {
        public int NextIndex { get; set; }

        [Required(ErrorMessage = "StaffType is required.")]
        public NomValue StaffType { get; set; }

        public NomValue Fcl { get; set; }

        [Required(ErrorMessage = "LicenceType is required.")]
        public NomValue LicenceType { get; set; }

        public string LicenceNumber { get; set; }

        public string ForeignLicenceNumber { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public NomValue Valid { get; set; }

        public PersonLicenceEditionDO[] Editions { get; set; }
    }
}
