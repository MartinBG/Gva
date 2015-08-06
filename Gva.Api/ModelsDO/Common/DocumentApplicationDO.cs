using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Applications;

namespace Gva.Api.ModelsDO.Common
{
    public class DocumentApplicationDO
    {
        public int ApplicationId { get; set; }

        public string OldDocumentNumber { get; set; }

        public string DocumentNumber { get; set; }

        [Required(ErrorMessage = "DocumentDate is required.")]
        public DateTime? DocumentDate { get; set; }

        [Required(ErrorMessage = "ApplicationType is required.")]
        public NomValue ApplicationType { get; set; }

        public NomValue ApplicationPaymentType { get; set; }

        public double? TaxAmount { get; set; }

        public string TaxNumber { get; set; }
        
        public NomValue Currency { get; set; }

        public string Notes { get; set; }

        public NomValue Stage { get; set; }

        public AppExaminationSystemDataDO ExaminationSystemData { get; set; }

        public App0001AdditionalDataDO App0001AdditionalData { get; set; }

        public App0004AdditionalDataDO App0004AdditionalData { get; set; }

        public App0008AdditionalDataDO App0008AdditionalData { get; set; }

        public FileDataDO ControlCard { get; set; }
    }
}
