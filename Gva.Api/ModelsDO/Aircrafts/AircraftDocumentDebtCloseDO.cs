using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentDebtCloseDO
    {
        public NomValue Inspector { get; set; }

        public DateTime? Date { get; set; }

        public string CaaDoc { get; set; }

        public DateTime? CaaDate { get; set; }

        public string CreditorDoc { get; set; }

        public DateTime? CreditorDate { get; set; }

        public string Notes { get; set; }
    }
}
