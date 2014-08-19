using System;
using System.Collections.Generic;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Airports
{
    public class AirportCertOperationalDO
    {
        public AirportCertOperationalDO()
        {
            this.Ext = new AirportCertOperationalExtDO();
            this.IncludedDocuments = new List<AirportIncludedDocDO>();
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

        public AirportCertOperationalExtDO Ext { get; set; }

        public List<AirportIncludedDocDO> IncludedDocuments { get; set; }
    }
}
