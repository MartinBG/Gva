using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertRegistrationDO
    {
        [Required(ErrorMessage = "Register is required.")]
        public NomValue Register { get; set; }

        [Required(ErrorMessage = "AircraftCertificateType is required.")]
        public NomValue AircraftCertificateType { get; set; }

        [Required(ErrorMessage = "CertNumber is required.")]
        public string CertNumber { get; set; }

        [Required(ErrorMessage = "CertDate is required.")]
        public DateTime? CertDate { get; set; }

        public NomValue AircraftNewOld { get; set; }

        public NomValue OperationType { get; set; }

        public NomValue Inspector { get; set; }

        public AircraftTypeCertDO TypeCert { get; set; }
    }
}
