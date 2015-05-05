using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentDebtCloseDO
    {
        public AircraftInspectorDO Inspector { get; set; }

        public DateTime? Date { get; set; }

        public string CaaDoc { get; set; }

        public DateTime? CaaDate { get; set; }

        public string TheirNumber { get; set; }

        public DateTime? TheirDate { get; set; }

        public string Notes { get; set; }
    }
}
