using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Aircrafts
{
    public class AircraftInspectorDO
    {
        public NomValue Inspector { get; set; }

        public NomValue Examiner { get; set; }

        public string Other { get; set; }
    }
}
