using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Applications;

namespace Gva.Api.ModelsDO.Common
{
    public class DocumentApplicationViewDO
    {
        public int ApplicationId { get; set; }

        public string OldDocumentNumber { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }

        public NomValue ApplicationType { get; set; }

        public NomValue ApplicationPaymentType { get; set; }

        public double? TaxAmount { get; set; }

        public string TaxNumber { get; set; }
        
        public NomValue Currency { get; set; }

        public string Notes { get; set; }

        public NomValue Stage { get; set; }

        public CaseDO Case { get; set; }
    }
}
