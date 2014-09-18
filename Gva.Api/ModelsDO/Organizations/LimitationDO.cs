using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class LimitationDO
    {
        // Lims147DO
        public int? SortOrder { get; set; }

        public string Lim147limitation { get; set; }

        public string Lim147limitationText { get; set; }

        // Lims145DO
        public NomValue Basic { get; set; }

        public NomValue Line { get; set; }

        public string Lim145limitation { get; set; }

        public string Lim145limitationText { get; set; }

        // LimsMGDO
        public string AircraftTypeGroup { get; set; }

        public string QualitySystem { get; set; }

        public NomValue Awapproval { get; set; }

        public NomValue Pfapproval { get; set; }
    }
}
