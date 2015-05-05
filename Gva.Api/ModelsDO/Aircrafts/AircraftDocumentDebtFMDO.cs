using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentDebtFMDO
    {
        public AircraftDocumentDebtFMDO()
        {
            this.IsActive = true;
        }

        public DateTime? RegDate { get; set; }

        [Required(ErrorMessage = "RegTime is required.")]
        public int? RegTime { get; set; }

        [Required(ErrorMessage = "AircraftDebtType is required.")]
        public NomValue AircraftDebtType { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }

        public NomValue AircraftApplicant { get; set; }

        public string TheirNumber { get; set; }

        public DateTime? TheirDate { get; set; }

        public string Notes { get; set; }

        public AircraftInspectorDO Inspector { get; set; }

        public bool IsActive { get; set; }

        public AircraftDocumentDebtCloseDO Close { get; set; }
    }
}
