using System;
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
            var matchResult = MatchPrefixAndNumber(@"^(2[4|5])(-)(\d+)", airworthiness.Content.DocumentNumber);

            GvaViewAircraftCert cert = new GvaViewAircraftCert();
            cert.LotId = airworthiness.Part.Lot.LotId;
            cert.PartIndex = airworthiness.Part.Index;
            cert.DocumentNumber = airworthiness.Content.DocumentNumber;
            cert.ParsedNumberWithoutPrefix = matchResult.Item3 ? matchResult.Item2 : (int?)null; ;
            cert.FormNumberPrefix = matchResult.Item1.HasValue ? matchResult.Item1.Value : (int?)null;

            return cert;
        }

        private GvaViewAircraftCert Create(PartVersion<AircraftCertNoiseDO> noise)
        {
            var matchResult = MatchPrefixAndNumber(@"^(45)(-)(\d+)", noise.Content.IssueNumber);

            GvaViewAircraftCert cert = new GvaViewAircraftCert();
            cert.LotId = noise.Part.Lot.LotId;
            cert.PartIndex = noise.Part.Index;
            cert.DocumentNumber = noise.Content.IssueNumber;
            cert.ParsedNumberWithoutPrefix = matchResult.Item3 ? matchResult.Item2 : (int?)null;
            cert.FormNumberPrefix = matchResult.Item1.HasValue ? matchResult.Item1.Value : (int?)null;

            return cert;
        }

        private Tuple<int?, int, bool> MatchPrefixAndNumber(string matchExpr, string number)
        {
            int? formNumberPrefix = (int?)null;
            string docNumber = null;
            Regex regExp = new Regex(matchExpr);
            Match match = regExp.Match(number);

            if (match.Success)
            {
                formNumberPrefix = int.Parse(match.Groups[1].Value);
                docNumber = match.Groups[3].Value;
            }

            int parsedDocNumber = 0;
            bool isParsingSuccessful = !string.IsNullOrEmpty(docNumber) ? int.TryParse(docNumber, out parsedDocNumber) : false;

            return new Tuple<int?, int, bool>(formNumberPrefix, parsedDocNumber, isParsingSuccessful);
        }
    }
}
