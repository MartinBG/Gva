using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertDeregFMDO
    {
        public DateTime? Date { get; set; }

        public NomValue Reason { get; set; }

        public string Text { get; set; }

        public string OrderNumber { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }

        public NomValue Country { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }

        public AircraftCertDeregExportFMDO Export { get; set; }
    }
}
