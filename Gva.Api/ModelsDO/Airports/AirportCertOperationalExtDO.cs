using System;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Airports
{
    public class AirportCertOperationalExtDO
    {
        public DateTime? Date { get; set; }

        public DateTime? ValidToDate { get; set; }

        public NomValue Inspector { get; set; }
    }
}
