using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class DocumentApplicationDO
    {
        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "DocumentDate is required.")]
        public DateTime? DocumentDate { get; set; }

        [Required(ErrorMessage = "ApplicationType is required.")]
        public NomValue ApplicationType { get; set; }

        public NomValue ApplicationPaymentType { get; set; }

        public double? TaxAmount { get; set; }

        public NomValue Currency { get; set; }

        public string Notes { get; set; }
    }
}
