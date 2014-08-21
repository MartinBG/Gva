using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class ExtDO
    {
        public NomValue Inspector { get; set; }

        public DateTime? Date { get; set; }

        public DateTime? ValidToDate { get; set; }
    }
}
