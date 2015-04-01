using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarlosAg.ExcelXmlWriter;

namespace Gva.MigrationTool
{
    public class FmOrgMatcher
    {
        public Tuple<Dictionary<string, int>, List<int>> Parse(
            string orgsMatchFilePath,
            Dictionary<string, int> personEgnToLotId,
            ConcurrentDictionary<string, int> orgNameEnToLotId,
            ConcurrentDictionary<string, int> orgUinToLotId)
        {
            Workbook book = new Workbook();

            using (var fs = new FileStream(orgsMatchFilePath, FileMode.Open, FileAccess.Read))
            {
                book.Load(fs);
            }

            Worksheet sheet = book.Worksheets["Organizations"];
            WorksheetRow headerRow = sheet.Table.Rows[0];

            Dictionary<string, int> headerToIndex = new Dictionary<string,int>();
            for (int i =0; i < headerRow.Cells.Count; i++)
            {
                var text = headerRow.Cells[i].Data.Text;
                if (!string.IsNullOrEmpty(text))
                {
                    headerToIndex.Add(text, i);
                }
            }

            int fmOrgNameIndex = headerToIndex["Organization FM"];
            int fmOrgIdIndex = headerToIndex["Organization FM Id"];
            int eikEgnIndex = headerToIndex["EIK"];
            int apexOrgNameIndex = headerToIndex["Organization Apex"];
            int matchTypeIndex = headerToIndex["Match Type"];

            Dictionary<string, int> fmNameEnToLotId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            List<int> notCreatedFmOrgIds = new List<int>();
            for (int i = 1; i < sheet.Table.Rows.Count; i++)
            {
                Func<int, string> getText = (ci) => sheet.Table.Rows[i].Cells[ci].Data.Text;

                string matchType = getText(matchTypeIndex);
                string fmOrgNameEn = getText(fmOrgNameIndex);

                switch (matchType)
                {
                    case "EGN":
                        string egn = getText(eikEgnIndex);

                        if (personEgnToLotId.ContainsKey(egn))
                        {
                            fmNameEnToLotId.Add(fmOrgNameEn, personEgnToLotId[egn]);
                        }
                        break;
                    case "EIK":
                        string eik = getText(eikEgnIndex);
                        if (orgUinToLotId.ContainsKey(eik))
                        {
                            fmNameEnToLotId.Add(fmOrgNameEn, orgUinToLotId[eik]);
                        }
                        break;
                    case "Org Name":
                    case "Org Trimmed Name":
                    case "Org Trimmed Name + Contains":
                        string apexOrgName = getText(apexOrgNameIndex);
                        if (orgNameEnToLotId.ContainsKey(apexOrgName))
                        {
                            fmNameEnToLotId.Add(fmOrgNameEn, orgNameEnToLotId[apexOrgName]);
                        }
                        break;
                    case "No Match":
                        notCreatedFmOrgIds.Add(int.Parse(getText(fmOrgIdIndex)));
                        break;
                    default:
                        break;
                }
            }

            return Tuple.Create(fmNameEnToLotId, notCreatedFmOrgIds);
        }
    }
}
