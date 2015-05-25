using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Aircraft
{
    public class AircraftCertProjection : Projection<GvaViewAircraftCert>
    {
        public AircraftCertProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Aircraft")
        {
        }

        public override IEnumerable<GvaViewAircraftCert> Execute(PartCollection parts)
        {
            var airworthinesses = parts.GetAll<AircraftCertAirworthinessDO>("aircraftCertAirworthinessesFM")
                .Where(a => !string.IsNullOrEmpty(a.Content.DocumentNumber) && 
                    (a.Content.AirworthinessCertificateType.Alias == "f25" || a.Content.AirworthinessCertificateType.Alias == "f24"));

            var noises = parts.GetAll<AircraftCertNoiseDO>("aircraftCertNoises")
                .Where(a => !string.IsNullOrEmpty(a.Content.IssueNumber));

            var airworthinessesResult = airworthinesses.Select(airworthiness => this.Create(airworthiness)) ?? new List<GvaViewAircraftCert>();
            var noisesResult = noises.Select(noise => this.Create(noise)) ?? new List<GvaViewAircraftCert>();

            return airworthinessesResult.Union(noisesResult);
        }

        private GvaViewAircraftCert Create(PartVersion<AircraftCertAirworthinessDO> airworthiness)
        {
            int? formNumberPrefix = (int?)null;
            string docNumber = null;
            Regex contains25or24 = new Regex(@"^(2[4|5])(-)(\d+)");
            Match match = contains25or24.Match(airworthiness.Content.DocumentNumber);

            if(match.Success)
            {
                formNumberPrefix = int.Parse(match.Groups[1].Value);
                string trimmedNumber = match.Groups[3].Value.TrimStart('0');
                docNumber =  !string.IsNullOrEmpty(trimmedNumber) ? trimmedNumber : "0";
            }

            string number = !string.IsNullOrEmpty(docNumber) ? docNumber : null;
            int parsedDocNumber;
            bool isParsingSuccessful = int.TryParse(docNumber, out parsedDocNumber);

            GvaViewAircraftCert cert = new GvaViewAircraftCert();
            cert.LotId = airworthiness.Part.Lot.LotId;
            cert.PartIndex = airworthiness.Part.Index;
            cert.DocumentNumber = airworthiness.Content.DocumentNumber;
            cert.ParsedNumberWithoutPrefix = isParsingSuccessful ? parsedDocNumber : (int?)null;
            cert.FormNumberPrefix = formNumberPrefix;

            return cert;
        }

        private GvaViewAircraftCert Create(PartVersion<AircraftCertNoiseDO> noise)
        {
            string docNumber = null;
            Regex contains45 = new Regex(@"^(45)(-)(\d+)");
            Match match = contains45.Match(noise.Content.IssueNumber);

             if(match.Success)
            {
                string trimmedNumber = match.Groups[3].Value.TrimStart('0');
                docNumber = !string.IsNullOrEmpty(trimmedNumber) ? trimmedNumber : "0";
            }

            string number = !string.IsNullOrEmpty(docNumber) ? docNumber : null;
            int parsedDocNumber;
            bool isParsingSuccessful = int.TryParse(number, out parsedDocNumber);

            GvaViewAircraftCert cert = new GvaViewAircraftCert();
            cert.LotId = noise.Part.Lot.LotId;
            cert.PartIndex = noise.Part.Index;
            cert.DocumentNumber = noise.Content.IssueNumber;
            cert.ParsedNumberWithoutPrefix = isParsingSuccessful ? parsedDocNumber : (int?)null;
            cert.FormNumberPrefix = 45;

            return cert;
        }
    }
}
