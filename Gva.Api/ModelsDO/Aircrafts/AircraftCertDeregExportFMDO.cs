using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftCertDeregExportFMDO
    {
        public string Text { get; set; }

        public string TextAlt { get; set; }

        public NomValue AircraftNewOld { get; set; }
    }
}
