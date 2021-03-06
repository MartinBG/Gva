﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonDocumentOtherDO_Old
    {
        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        [Required(ErrorMessage = "DocumentPublisher is required.")]
        public string DocumentPublisher { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        [Required(ErrorMessage = "DocumentType is required.")]
        public int? DocumentTypeId { get; set; }

        [Required(ErrorMessage = "DocumentRole is required.")]
        public int? DocumentRoleId { get; set; }

        [Required(ErrorMessage = "Valid is required.")]
        public int? ValidId { get; set; }

        public string Notes { get; set; }
    }
}
