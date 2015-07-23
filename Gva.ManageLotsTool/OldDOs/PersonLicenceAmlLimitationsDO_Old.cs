using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonLicenceAmlLimitationsDO_Old
    {
        public PersonLicenceAmlLimitationsDO_Old()
        {
            this.At_a_Ids = new List<NomValue>();
            this.At_b1_Ids = new List<NomValue>();
            this.Ap_a_Ids = new List<NomValue>();
            this.Ap_b1_Ids = new List<NomValue>();
            this.Ht_a_Ids = new List<NomValue>();
            this.Ht_b1_Ids = new List<NomValue>();
            this.Hp_a_Ids = new List<NomValue>();
            this.Hp_b1_Ids = new List<NomValue>();
            this.Avionics_Ids = new List<NomValue>();
            this.Pe_b3_Ids = new List<NomValue>();
        }

        public List<NomValue> At_a_Ids { get; set; }

        public List<NomValue> At_b1_Ids { get; set; }

        public List<NomValue> Ap_a_Ids { get; set; }

        public List<NomValue> Ap_b1_Ids { get; set; }

        public List<NomValue> Ht_a_Ids { get; set; }

        public List<NomValue> Ht_b1_Ids { get; set; }

        public List<NomValue> Hp_a_Ids { get; set; }

        public List<NomValue> Hp_b1_Ids { get; set; }

        public List<NomValue> Avionics_Ids { get; set; }

        public List<NomValue> Pe_b3_Ids { get; set; }
    }
}
