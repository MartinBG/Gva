using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class IncludedDocumentDO
    {
        public NomValue Inspector { get; set; }

        public DateTime? approvalDate { get; set; }

    }
}
