using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftTypeCertDO
    {
        [Required(ErrorMessage = "AircraftTypeCertificateType is required.")]
        public NomValue AircraftTypeCertificateType { get; set; }

        public string CertNumber { get; set; }

        public DateTime? CertDate { get; set; }

        public string CertRelease { get; set; }

        public NomValue Contry { get; set; }

    }
}
