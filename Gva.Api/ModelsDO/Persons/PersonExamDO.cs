using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonExamDO
    {
        public PersonExamDO()
        {
            this.CommonQuestions = new bool[10, 4];
            this.SpecializedQuestions = new bool[20, 4];
            this.Inspectors = new List<NomValue>();
        }

        [Required(ErrorMessage = "ExamDate is required.")]
        public DateTime? ExamDate { get; set; }

        [Required(ErrorMessage = "CommonQuestion is required.")]
        public NomValue CommonQuestion { get; set; }

        public bool[,] CommonQuestions { get; set; }

        [Required(ErrorMessage = "SpecializedQuestion is required.")]
        public NomValue SpecializedQuestion { get; set; }

        public bool[,] SpecializedQuestions { get; set; }

        public List<NomValue> Inspectors { get; set; }

        public int? Score { get; set; }

        [Required(ErrorMessage = "SuccessThreshold is required.")]
        [Range(1, 100, ErrorMessage = "SuccessThreshold isn't between 1 and 100.")]
        public int? SuccessThreshold { get; set; }

        public NomValue Passed { get; set; }
    }
}
