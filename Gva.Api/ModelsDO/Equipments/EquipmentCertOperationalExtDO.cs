using System;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Equipments
{
    public class EquipmentCertOperationalExtDO
    {
        public DateTime? Date { get; set; }

        public DateTime? ValidToDate { get; set; }

        public NomValue Inspector { get; set; }
     }
}
