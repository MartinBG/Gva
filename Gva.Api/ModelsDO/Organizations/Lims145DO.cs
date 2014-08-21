using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Organizations
{
    public class Lims145DO
    {
        public string Lim145limitation { get; set; }

        public string Lim145limitationText { get; set; }

        public NomValue Basic { get; set; }

        public NomValue Line { get; set; }
    }
}
