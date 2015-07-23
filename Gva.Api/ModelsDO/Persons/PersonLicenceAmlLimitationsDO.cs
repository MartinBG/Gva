using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceAmlLimitationsDO
    {
        public PersonLicenceAmlLimitationsDO()
        {
            this.At_a_Ids = new List<int>();
            this.At_b1_Ids = new List<int>();
            this.Ap_a_Ids = new List<int>();
            this.Ap_b1_Ids = new List<int>();
            this.Ht_a_Ids = new List<int>();
            this.Ht_b1_Ids = new List<int>();
            this.Hp_a_Ids = new List<int>();
            this.Hp_b1_Ids = new List<int>();
            this.Avionics_Ids = new List<int>();
            this.Pe_b3_Ids = new List<int>();
        }

        public List<int> At_a_Ids { get; set; }

        public List<int> At_b1_Ids { get; set; }

        public List<int> Ap_a_Ids { get; set; }

        public List<int> Ap_b1_Ids { get; set; }

        public List<int> Ht_a_Ids { get; set; }

        public List<int> Ht_b1_Ids { get; set; }

        public List<int> Hp_a_Ids { get; set; }

        public List<int> Hp_b1_Ids { get; set; }

        public List<int> Avionics_Ids { get; set; }

        public List<int> Pe_b3_Ids { get; set; }
    }
}
