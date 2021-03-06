﻿using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftDocumentDebtFMViewDO
    {
        public DateTime? RegDate { get; set; }

        public NomValue AircraftDebtType { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDate { get; set; }

        public NomValue AircraftApplicant { get; set; }

        public AircraftInspectorDO Inspector { get; set; }
    }
}