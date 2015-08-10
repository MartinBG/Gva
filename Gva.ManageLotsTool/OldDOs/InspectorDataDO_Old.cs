using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class InspectorDataDO_Old
    {
        public NomValue Caa { get; set; }

        [Required(ErrorMessage = "ExaminerCode is required.")]
        public string ExaminerCode { get; set; }

        public string StampNum { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public NomValue Valid { get; set; }
    }
}
