using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;
using System.Data.SqlClient;
using System.Data.Common;
using CarlosAg.ExcelXmlWriter;
using Gva.OrgMatchingTool.Model;
using Gva.MigrationTool;

namespace Gva.OrgMatchingTool
{

    class Program
    {
        private const string oracleConnStr = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=192.168.0.19)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=VENI.CAA)));User Id=system;Password=DBSYSTEMVENI;";
        private const string sqlConnStr = "Data Source=.\\;Initial Catalog=GvaAircraft;Integrated Security=True;MultipleActiveResultSets=True";


        public static string TrimName(string originalText)
        {
            return originalText.Replace("\"", "")
                .Replace(".", "")
                .Replace(",", "")
                .Replace(";", "")
                .Replace("Jsc", "")
                .Replace("Ltd", "")
                .Replace("a/k", "")
                .Replace("&", "")
                .Replace("-", "")
                .Replace("_", "")
                .Replace("“", "")
                .Replace("”", "")
                .Replace(" ", "")
                .ToLower();
        }

        static void Main(string[] args)
        {
            OracleConnection oracleConn = new OracleConnection(oracleConnStr);
            SqlConnection sqlConn = new SqlConnection(sqlConnStr);

            try
            {
                oracleConn.Open();
                sqlConn.Open();

                #region queries
                var FMOrgs = sqlConn.CreateStoreCommand("select * from orgs")
               .Materialize(r =>
                   new Organization
                   {
                       EIK = r.Field<string>("t_EIK_EGN"),
                       TrimmedName = TrimName(r.Field<string>("tNameEN")),
                       Name = r.Field<string>("tNameEN")
                   })
               .ToList();
                FMOrgs = FMOrgs.Where(e => e.TrimmedName != "").ToList();

                List<Organization> MatchedOrgs = new List<Organization>();

                var ApexOrgs = oracleConn.CreateStoreCommand("select * from CAA_DOC.Firm")
               .Materialize(r =>
                   new ApexOrg
                {
                    Name = r.Field<string>("NAME_TRANS"),
                    TrimmedName = TrimName(r.Field<string>("NAME_TRANS")),
                    EIK = r.Field<string>("BULSTAT")
                })
               .ToList();

                var ApexPersons = oracleConn.CreateStoreCommand("select * from CAA_DOC.Person")
               .Materialize(r =>
                   new ApexPerson
                {
                    Name = r.Field<string>("NAME_TRANS") + " " + r.Field<string>("SURNAME_TRANS") + " " + r.Field<string>("FAMILY_TRANS"),
                    NameBgEn = r.Field<string>("NAME_TRANS") + " " + r.Field<string>("SURNAME_TRANS") + " " + r.Field<string>("FAMILY_TRANS") +
                        "(" + r.Field<string>("NAME") + " " + r.Field<string>("SURNAME") + " " + r.Field<string>("FAMILY") + ")",
                    TrimmedName = TrimName(r.Field<string>("NAME_TRANS") + r.Field<string>("SURNAME_TRANS") + r.Field<string>("FAMILY_TRANS")),
                    EIK = r.Field<string>("EGN")
                })
               .ToList();
                #endregion

                int totalMatches = 0;

                #region By EIK\EGN
                foreach (var org in FMOrgs)
                {
                    org.MatchCount = 0;
                    bool matchFound = false;
                    if (org.EIK.Length == 10)
                    {
                        foreach (var person in ApexPersons)
                        {
                            if (org.EIK == person.EIK)
                            {
                                matchFound = true;
                                org.MatchCount++;
                                org.MatchedPersonTrimmedName = person.TrimmedName;
                                org.MatchedPersonName = person.Name;
                                org.MatchedPersonNameBgEn = person.NameBgEn;
                                org.MatchType = "EGN";
                            }
                        }
                        if (matchFound)
                        {
                            totalMatches++;
                        }
                    }
                    else
                    {
                        foreach (var apexOrg in ApexOrgs)
                        {
                            if (org.EIK == apexOrg.EIK)
                            {
                                matchFound = true;
                                org.MatchCount++;
                                org.MatchedOrgTrimmedName = apexOrg.TrimmedName;
                                org.MatchedOrgName = apexOrg.Name;
                                org.MatchType = "EIK";
                            }
                        }
                        if (matchFound)
                        {
                            totalMatches++;
                        }
                    }
                }
                MatchedOrgs = FMOrgs.Where(e => e.MatchType != null).ToList();
                FMOrgs = FMOrgs.Where(e => e.MatchType == null).ToList();
                #endregion

                #region By Person Name
                foreach (var org in FMOrgs)
                {
                    org.MatchCount = 0;
                    bool matchFound = false;
                    foreach (var person in ApexPersons)
                    {
                        if (org.Name == person.Name)
                        {
                            matchFound = true;
                            org.MatchCount++;
                            org.MatchedPersonTrimmedName = person.TrimmedName;
                            org.MatchedPersonName = person.Name;
                            org.MatchedPersonNameBgEn = person.NameBgEn;
                            org.MatchType = "Person Name";
                        }
                    }
                    if (matchFound)
                    {
                        totalMatches++;
                    }
                }
                MatchedOrgs.AddRange(FMOrgs.Where(e => e.MatchType != null).ToList());
                FMOrgs = FMOrgs.Where(e => e.MatchType == null).ToList();
                #endregion

                #region By Org Name
                foreach (var org in FMOrgs)
                {
                    org.MatchCount = 0;
                    bool matchFound = false;
                    foreach (var apexOrg in ApexOrgs)
                    {
                        if (org.Name == apexOrg.Name)
                        {
                            matchFound = true;
                            org.MatchCount++;
                            org.MatchedOrgTrimmedName = apexOrg.TrimmedName;
                            org.MatchedOrgName = apexOrg.Name;
                            org.MatchType = "Org Name";
                        }
                    }
                    if (matchFound)
                    {
                        totalMatches++;
                    }
                }
                MatchedOrgs.AddRange(FMOrgs.Where(e => e.MatchType != null).ToList());
                FMOrgs = FMOrgs.Where(e => e.MatchType == null).ToList();
                #endregion

                #region By Trimmed Person Name
                foreach (var org in FMOrgs)
                {
                    org.MatchCount = 0;
                    bool matchFound = false;
                    foreach (var person in ApexPersons)
                    {
                        if (org.TrimmedName == person.TrimmedName)
                        {
                            matchFound = true;
                            org.MatchCount++;
                            org.MatchedPersonTrimmedName = person.TrimmedName;
                            org.MatchedPersonName = person.Name;
                            org.MatchedPersonNameBgEn = person.NameBgEn;
                            org.MatchType = "Person Trimmed Name";
                        }
                    }
                    if (matchFound)
                    {
                        totalMatches++;
                    }
                }
                MatchedOrgs.AddRange(FMOrgs.Where(e => e.MatchType != null).ToList());
                FMOrgs = FMOrgs.Where(e => e.MatchType == null).ToList();
                #endregion

                #region By Org Trimmed Name
                foreach (var org in FMOrgs)
                {
                    org.MatchCount = 0;
                    bool matchFound = false;
                    foreach (var apexOrg in ApexOrgs)
                    {
                        if (org.TrimmedName == apexOrg.TrimmedName)
                        {
                            matchFound = true;
                            org.MatchCount++;
                            org.MatchedOrgTrimmedName = apexOrg.TrimmedName;
                            org.MatchedOrgName = apexOrg.Name;
                            org.MatchType = "Org Trimmed Name";
                        }
                    }
                    if (matchFound)
                    {
                        totalMatches++;
                    }
                }
                MatchedOrgs.AddRange(FMOrgs.Where(e => e.MatchType != null).ToList());
                FMOrgs = FMOrgs.Where(e => e.MatchType == null).ToList();
                #endregion

                #region By Org Trimmed Name + Contains
                foreach (var org in FMOrgs)
                {
                    org.MatchCount = 0;
                    bool matchFound = false;
                    foreach (var apexOrg in ApexOrgs)
                    {
                        if ((org.TrimmedName.Contains(apexOrg.TrimmedName) || apexOrg.TrimmedName.Contains(org.TrimmedName)) && org.TrimmedName != "")
                        {
                            matchFound = true;
                            org.MatchCount++;
                            org.MatchedOrgTrimmedName = apexOrg.TrimmedName;
                            org.MatchedOrgName = apexOrg.Name;
                            org.MatchType = "Org Trimmed Name + Contains";
                        }
                    }
                    if (matchFound)
                    {
                        totalMatches++;
                    }
                }
                MatchedOrgs.AddRange(FMOrgs.Where(e => e.MatchType != null).ToList());
                FMOrgs = FMOrgs.Where(e => e.MatchType == null).ToList();
                #endregion

                #region Fill Excell
                Workbook book = new Workbook();
                Worksheet sheet = book.Worksheets.Add("Organizations");
                WorksheetRow row = sheet.Table.Rows.Add();
                row.Cells.Add("Match Type");
                row.Cells.Add("Match Count");
                row.Cells.Add("EIK");
                row.Cells.Add("Organization FM");
                row.Cells.Add("Organization Apex");
                row.Cells.Add("Person Apex");
                foreach (var org in MatchedOrgs)
                {
                    WorksheetRow nextRow = sheet.Table.Rows.Add();
                    nextRow.Cells.Add(org.MatchType);
                    nextRow.Cells.Add(org.MatchCount.ToString());
                    nextRow.Cells.Add(org.EIK);
                    nextRow.Cells.Add(org.Name);
                    nextRow.Cells.Add(org.MatchedOrgName);
                    nextRow.Cells.Add(org.MatchedPersonNameBgEn);
                }
                foreach (var org in FMOrgs)
                {
                    WorksheetRow nextRow = sheet.Table.Rows.Add();
                    nextRow.Cells.Add("No Match");
                    nextRow.Cells.Add("0");
                    nextRow.Cells.Add(org.EIK);
                    nextRow.Cells.Add(org.Name);
                    nextRow.Cells.Add(org.MatchedOrgName);
                    nextRow.Cells.Add(org.MatchedPersonName);
                }
                #endregion

                Console.WriteLine(totalMatches + "/" + (FMOrgs.Count + totalMatches));
                Console.ReadKey(true);
                book.Save(@"D:\Organizations.xls");

            }
            catch (OracleException e)
            {
                Console.WriteLine("Exception Message: " + e.Message);
                Console.WriteLine("Exception Source: " + e.Source);
            }
            catch (SqlException e)
            {
                Console.WriteLine("Exception Message: " + e.Message);
                Console.WriteLine("Exception Source: " + e.Source);
            }
        }


    }
}
