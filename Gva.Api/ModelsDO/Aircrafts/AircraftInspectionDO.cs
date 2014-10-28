using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System.Collections.Generic;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftInspectionDO
    {
        public AircraftInspectionDO()
        {
            this.InspectionDetails = new List<InspectionDetailDO>();
            this.Disparities = new List<DisparityDO>();
            this.Inspectors = new List<NomValue>();
        }

        public string DocumentNumber { get; set; }

        public NomValue AuditPart { get; set; }

        [Required(ErrorMessage = "AuditReason is required.")]
        public NomValue AuditReason { get; set; }

        [Required(ErrorMessage = "AuditType is required.")]
        public NomValue AuditType { get; set; }

        [Required(ErrorMessage = "AuditState is required.")]
        public NomValue AuditState { get; set; }

        [Required(ErrorMessage = "Notification is required.")]
        public NomValue Notification { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "InspectionPlace is required.")]
        public string InspectionPlace { get; set; }

        [Required(ErrorMessage = "StartDate is required.")]
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public List<InspectionDetailDO> InspectionDetails { get; set; }

        public List<DisparityDO> Disparities { get; set; }

        public List<NomValue> Inspectors { get; set; }

        //AircraftInspection specific fields
        public DateTime? InspectionFrom { get; set; }

        public DateTime? InspectionTo { get; set; }

        public FileDataDO ControlCard { get; set; }
    }
}
