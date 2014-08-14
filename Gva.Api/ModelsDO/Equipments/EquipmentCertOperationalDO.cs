using System;
using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Equipments
{
    public class EquipmentCertOperationalDO
    {
        public EquipmentCertOperationalDO()
        {
            this.Ext = new EquipmentCertOperationalExtDO();
            this.IncludedDocuments = new List<EquipmentIncludedDocDO>();
        }

        public DateTime? IssueDate { get; set; }

        public string IssueNumber { get; set; }

        public DateTime? ValidToDate { get; set; }

        public NomValue Organization { get; set; }

        public NomValue Inspector { get; set; }

        public NomValue Valid { get; set; }

        public DateTime? RevokeDate { get; set; }

        public NomValue RevokeInspector { get; set; }

        public string RevokeCause { get; set; }

        public EquipmentCertOperationalExtDO Ext { get; set; }

        public List<EquipmentIncludedDocDO> IncludedDocuments { get; set; }
    }
}
