using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationCertAirCarrierDO
    {
        public OrganizationCertAirCarrierDO()
        {
            this.IncludedDocuments = new List<IncludedDocumentDO>();
            this.Aircrafts = new List<NomValue>();
            this.AircarrierServices = new List<NomValue>();
        }

        public string CertNumber { get; set; }

        public DateTime? ValidToDate { get; set; }

        public DateTime? IssueDate { get; set; }

        public NomValue Audit { get; set; }

        public NomValue Organization { get; set; }

        public NomValue Airport { get; set; }

        public NomValue Inspector { get; set; }

        public NomValue Valid { get; set; }

        public List<NomValue> Aircrafts { get; set; }

        public List<NomValue> AircarrierServices { get; set; }

        public ExtDO Ext { get; set; }

        public List<IncludedDocumentDO> IncludedDocuments { get; set; }

        public DateTime? RevokeDate { get; set; }

        public NomValue Revokeinspector { get; set; }

        public string RevokeCause { get; set; }
    }
}
