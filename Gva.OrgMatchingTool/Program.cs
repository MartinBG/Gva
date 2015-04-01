using System;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;
using CarlosAg.ExcelXmlWriter;
using Gva.Api.CommonUtils;
using Gva.OrgMatchingTool.Model;
using Oracle.ManagedDataAccess.Client;

namespace Gva.OrgMatchingTool
{

    class Program
    {
        private const string oracleConnStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.19)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=VENI.CAA)));User Id=system;Password=DBSYSTEMVENI;";
        private const string sqlConnStr = "Data Source=.\\;Initial Catalog=GvaAircraft;Integrated Security=True;MultipleActiveResultSets=True";


        public static string TrimName(string text)
        {
            Func<string, string, string> removeWholeWordIgnoreCase = (s, p) => Regex.Replace(s, @"\b" + p + @"\b", "", RegexOptions.IgnoreCase);

            text = removeWholeWordIgnoreCase(text, "jsc");
            text = removeWholeWordIgnoreCase(text, "ltd");
            text = removeWholeWordIgnoreCase(text, "limited");
            text = removeWholeWordIgnoreCase(text, "a/k");
            text = removeWholeWordIgnoreCase(text, "ood");
            text = removeWholeWordIgnoreCase(text, "eood");
            text = removeWholeWordIgnoreCase(text, "et");
            text = Regex.Replace(text, "[^а-зА-Зa-zA-Z0-9]", "", RegexOptions.Singleline); //remove any non word characters
            return text.ToLowerInvariant();
        }

        static void Main(string[] args)
        {
            OracleConnection oracleConn = new OracleConnection(oracleConnStr);
            SqlConnection sqlConn = new SqlConnection(sqlConnStr);

            oracleConn.Open();
            sqlConn.Open();

            var fmOrgs = sqlConn.CreateStoreCommand("select * from orgs")
            .Materialize(r =>
                new
                {
                    EIK = r.Field<string>("t_EIK_EGN"),
                    Name = r.Field<string>("tNameEN"),
                    Id = r.Field<int>("nOrgID")
                })
            .Where(o => !string.IsNullOrWhiteSpace(o.Name))
            //skip duplicates
            .GroupBy(o => o.Name, StringComparer.InvariantCultureIgnoreCase)
            .Select(g =>
                new FmOrg
                {
                    EIK = g.Select(r => r.EIK).Where(eik => !string.IsNullOrWhiteSpace(eik)).Distinct().SingleOrDefault(),
                    TrimmedName = TrimName(g.Key),
                    Name = g.Key,
                    Id = g.Select(r => r.Id).First()
                })
            .OrderBy(o => o.Name)
            .ToList();

            var ApexOrgs = oracleConn.CreateStoreCommand("select * from CAA_DOC.Firm")
            .Materialize(r =>
                new ApexOrg
            {
                Name = r.Field<string>("NAME_TRANS"),
                TrimmedName = TrimName(r.Field<string>("NAME_TRANS")),
                EIK = r.Field<string>("BULSTAT"),
                Id = r.Field<int>("ID")
            })
            .ToList();

            var ApexPersons = oracleConn.CreateStoreCommand("select * from CAA_DOC.Person")
            .Materialize(r =>
                new ApexPerson
            {
                Name = r.Field<string>("NAME_TRANS") + " " + r.Field<string>("SURNAME_TRANS") + " " + r.Field<string>("FAMILY_TRANS"),
                NameBg = r.Field<string>("NAME") + " " + r.Field<string>("SURNAME") + " " + r.Field<string>("FAMILY"),
                TrimmedName = TrimName(r.Field<string>("NAME_TRANS") + r.Field<string>("SURNAME_TRANS") + r.Field<string>("FAMILY_TRANS")),
                EIK = r.Field<string>("EGN"),
                Id = r.Field<int>("ID")
            })
            .ToList();

            var egnMatches =
                (from fmO in fmOrgs
                join p in ApexPersons on fmO.EIK equals p.EIK
                select new OrgMatch
                {
                    ApexPersonId = p.Id,
                    FmOrgName = fmO.Name,
                    EIK = fmO.EIK,
                    ApexPersonNameEn = p.Name,
                    ApexPersonNameBg = p.NameBg,
                    MatchType = "EGN"
                }).ToList();

            fmOrgs = fmOrgs.Where(fmO => !egnMatches.Any(m => m.FmOrgName == fmO.Name)).ToList();

            var eikMatches =
                (from fmO in fmOrgs
                join aO in ApexOrgs on fmO.EIK equals aO.EIK
                select new OrgMatch
                {
                    ApexOrgId = aO.Id,
                    FmOrgName = fmO.Name,
                    EIK = fmO.EIK,
                    ApexOrgNameEn = aO.Name,
                    MatchType = "EIK"
                }).ToList();

            fmOrgs = fmOrgs.Where(fmO => !eikMatches.Any(m => m.FmOrgName == fmO.Name)).ToList();

            var personNameMatches =
                (from fmO in fmOrgs
                join p in ApexPersons on fmO.Name equals p.Name
                select new OrgMatch
                {
                    ApexPersonId = fmO.Id,
                    FmOrgName = fmO.Name,
                    EIK = fmO.EIK,
                    ApexPersonNameEn = p.Name,
                    ApexPersonNameBg = p.NameBg,
                    MatchType = "Person Name"
                }).ToList();

            fmOrgs = fmOrgs.Where(fmO => !personNameMatches.Any(m => m.FmOrgName == fmO.Name)).ToList();

            var orgNameMatches =
                (from fmO in fmOrgs
                join aO in ApexOrgs on fmO.Name equals aO.Name
                select new OrgMatch
                {
                    FmOrgId = fmO.Id,
                    FmOrgName = fmO.Name,
                    EIK = fmO.EIK,
                    ApexOrgNameEn = aO.Name,
                    MatchType = "Org Name"
                }).ToList();

            fmOrgs = fmOrgs.Where(fmO => !orgNameMatches.Any(m => m.FmOrgName == fmO.Name)).ToList();

            var personTrimmedNameMatches =
                (from fmO in fmOrgs
                join p in ApexPersons on fmO.TrimmedName equals p.TrimmedName
                select new OrgMatch
                {
                    ApexPersonId = fmO.Id,
                    FmOrgName = fmO.Name,
                    EIK = fmO.EIK,
                    ApexPersonNameEn = p.Name,
                    ApexPersonNameBg = p.NameBg,
                    MatchType = "Person Trimmed Name"
                }).ToList();

            fmOrgs = fmOrgs.Where(fmO => !personTrimmedNameMatches.Any(m => m.FmOrgName == fmO.Name)).ToList();

            var orgTrimmedNameMatches =
                (from fmO in fmOrgs
                join aO in ApexOrgs on fmO.TrimmedName equals aO.TrimmedName
                select new OrgMatch
                {
                    FmOrgId = fmO.Id,
                    ApexOrgId = aO.Id,
                    FmOrgName = fmO.Name,
                    EIK = fmO.EIK,
                    ApexOrgNameEn = aO.Name,
                    MatchType = "Org Trimmed Name"
                }).ToList();

            fmOrgs = fmOrgs.Where(fmO => !orgTrimmedNameMatches.Any(m => m.FmOrgName == fmO.Name)).ToList();

            var matches = egnMatches
                .Concat(eikMatches)
                .Concat(personNameMatches)
                .Concat(orgNameMatches)
                .Concat(personTrimmedNameMatches)
                .Concat(orgTrimmedNameMatches)
                //TODO take the first org in case of duplicated names, should be investigated
                .GroupBy(m => m.FmOrgName)
                .Select(g => g.First());

            #region Fill Excell
            Workbook book = new Workbook();
            Worksheet sheet = book.Worksheets.Add("Organizations");
            WorksheetRow row = sheet.Table.Rows.Add();
            row.Cells.Add("Match Type");
            row.Cells.Add("EIK");
            row.Cells.Add("Organization FM");
            row.Cells.Add("Organization FM Id");
            row.Cells.Add("Organization Apex");
            row.Cells.Add("Organization Apex Id");
            row.Cells.Add("Person Apex");
            row.Cells.Add("Person Apex Id");

            foreach (var m in matches)
            {
                WorksheetRow nextRow = sheet.Table.Rows.Add();
                nextRow.Cells.Add(m.MatchType);
                nextRow.Cells.Add(m.EIK);
                nextRow.Cells.Add(m.FmOrgName);
                nextRow.Cells.Add(m.FmOrgId.HasValue? m.FmOrgId.Value.ToString() : "");
                nextRow.Cells.Add(m.ApexOrgNameEn);
                nextRow.Cells.Add(m.ApexOrgId.HasValue ? m.ApexOrgId.Value.ToString() : "");
                if (!string.IsNullOrEmpty(m.ApexPersonNameEn) || !string.IsNullOrEmpty(m.ApexPersonNameBg))
                {
                    nextRow.Cells.Add(m.ApexPersonNameEn + "(" + m.ApexPersonNameBg + ")");
                }
                else
                {
                    nextRow.Cells.Add("");
                }
                nextRow.Cells.Add(m.ApexPersonId.HasValue ? m.ApexPersonId.Value.ToString() : "");
            }

            foreach (var org in fmOrgs)
            {
                WorksheetRow nextRow = sheet.Table.Rows.Add();
                nextRow.Cells.Add("No Match");
                nextRow.Cells.Add(org.EIK);
                nextRow.Cells.Add(org.Name);
                nextRow.Cells.Add(org.Id.ToString());
            }
            #endregion

            book.Save(@"Organizations.xls");

            int matched = matches.Count();
            int unmatched = fmOrgs.Count();
            Console.WriteLine(matched + "/" + (matched + unmatched));
        }
    }
}
