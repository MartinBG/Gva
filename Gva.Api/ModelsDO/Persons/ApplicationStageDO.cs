﻿using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class AplicationStageDO
    {
        public int ApplicationId { get; set; }

        public string[] StageAliases { get; set; }
    }
}
