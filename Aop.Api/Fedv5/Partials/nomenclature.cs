using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace NOMv5
{
    public partial class nomenclature
    {
        [XmlIgnore]
        public nom this[string nomId]
        {
            get
            {
                if (String.IsNullOrWhiteSpace(nomId) || !this.nom.Any(e => nomId.Equals(e.id)))
                {
                    throw new Exception("The noms of this nomenclature do not contain an element with id " + nomId);
                }
                else
                {
                    nom nom = this.nom.FirstOrDefault(e => nomId.Equals(e.id));
                    return nom;
                }
            }
            set
            {
                throw new Exception("The noms cannot be set.");
            }
        }
    }
}
