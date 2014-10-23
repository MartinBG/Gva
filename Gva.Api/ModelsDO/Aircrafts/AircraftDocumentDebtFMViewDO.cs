using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentDebtFMViewDO
    {
        public int? RegistrationCertNumber { get; set; }

        public int? RegistrationActNumber { get; set; }

        public DateTime? RegDate { get; set; }

        public NomValue AircraftDebtType { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }

        public NomValue AircraftCreditor { get; set; }

        public NomValue Inspector { get; set; }
    }
}