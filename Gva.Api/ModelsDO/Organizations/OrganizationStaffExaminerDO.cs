using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Organizations
{
    public class OrganizationStaffExaminerDO
    {
        public OrganizationStaffExaminerDO()
        {
            this.ApprovedAircrafts = new List<OrganizationApprovedAircraftsDO>();
        }

        [Required(ErrorMessage = "ExaminerCode is required.")]
        public string ExaminerCode { get; set; }

        [Required(ErrorMessage = "Person is required.")]
        public NomValue Person { get; set; }

        [Required(ErrorMessage = "PermitedAW is required.")]
        public NomValue PermitedAW { get; set; }

        [Required(ErrorMessage = "PermitedCheck is required.")]
        public NomValue PermitedCheck { get; set; }

        public string StampNum { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }

        public List<OrganizationApprovedAircraftsDO> ApprovedAircrafts { get; set; }
    }
}
