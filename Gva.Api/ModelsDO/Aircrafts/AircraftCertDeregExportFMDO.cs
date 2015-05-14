using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertDeregExportFMDO
    {
        public string Text { get; set; }

        public string TextAlt { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }

        public bool? AircraftNewOld { get; set; }
    }
}
