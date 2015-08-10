using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class InspectorDataDO
    {
        public int? CaaId { get; set; }

        [Required(ErrorMessage = "ExaminerCode is required.")]
        public string ExaminerCode { get; set; }

        public string StampNum { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public int? ValidId { get; set; }
    }
}
