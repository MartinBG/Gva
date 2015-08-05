using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentOtherDO
    {
        public string DocumentNumber { get; set; }

        public DateTime DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public string DocumentPublisher { get; set; }

        public int? OtherDocumentTypeId { get; set; }

        public int? OtherDocumentRoleId { get; set; }

        public int? ValidId { get; set; }

        public string Notes { get; set; }
    }
}
