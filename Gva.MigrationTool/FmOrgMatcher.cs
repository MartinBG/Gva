using System;
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
        public Tuple<Dictionary<string, int>, Dictionary<string, int>> Parse(
            string orgsMatchFilePath,
            Dictionary<string, int> personEgnToLotId,
            Dictionary<string, int> orgNameEnToLotId,
            Dictionary<string, int> orgUinToLotId)
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
            int eikEgnIndex = headerToIndex["EIK"];
            int apexOrgNameIndex = headerToIndex["Organization Apex"];
            int matchTypeIndex = headerToIndex["Match Type"];

            Dictionary<string, int> fmOrgNameEnToPersonLotId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
            Dictionary<string, int> fmOrgNameEnToOrgLotId = new Dictionary<string, int>(StringComparer.InvariantCultureIgnoreCase);
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
                            fmOrgNameEnToPersonLotId.Add(fmOrgNameEn, personEgnToLotId[egn]);
                        }
                        break;
                    case "EIK":
                        string eik = getText(eikEgnIndex);
                        if (orgUinToLotId.ContainsKey(eik))
                        {
                            fmOrgNameEnToOrgLotId.Add(fmOrgNameEn, orgUinToLotId[eik]);
                        }
                        break;
                    case "Org Name":
                    case "Org Trimmed Name":
                    case "Org Trimmed Name + Contains":
                        string apexOrgName = getText(apexOrgNameIndex);
                        if (orgNameEnToLotId.ContainsKey(apexOrgName))
                        {
                            fmOrgNameEnToOrgLotId.Add(fmOrgNameEn, orgNameEnToLotId[apexOrgName]);
                        }
                        break;
                    default:
                        break;
                }
            }

            return Tuple.Create(fmOrgNameEnToPersonLotId, fmOrgNameEnToOrgLotId);
        }
    }
}
