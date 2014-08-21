using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class Lims147DO
    {
        public string Lim147limitation { get; set; }

        public string Lim147limitationText { get; set; }

        public int? SortOrder { get; set; }
    }
}
