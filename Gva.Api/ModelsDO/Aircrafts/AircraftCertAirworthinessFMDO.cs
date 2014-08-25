using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertAirworthinessFMDO
    {
        public AircraftCertAirworthinessFMDO()
        {
            this.Reviews = new List<AircraftCertAirworthinessReviewFMDO>();
        }

        [Required(ErrorMessage = "AirworthinessCertificateType is required.")]
        public NomValue AirworthinessCertificateType { get; set; }

        [Required(ErrorMessage = "Registration is required.")]
        public PartSelectDO Registration { get; set; }

        [Required(ErrorMessage = "DocumentNumber is required.")]
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "IssueDate is required.")]
        public DateTime? IssueDate { get; set; }

        public List<AircraftCertAirworthinessReviewFMDO> Reviews { get; set; }
    }
}
