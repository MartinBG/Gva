using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using ClosedXML.Excel;
using Common.Api.Models;
using Common.Data;
using Common.Linq;
using Gva.Api.CommonUtils;
using Gva.Api.Models;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Persons.Reports;

namespace Gva.Api.Repositories.AircraftRepository
{
    public class PersonsReportRepository : IPersonsReportRepository
    {
        public List<PersonReportDocumentDO> GetDocuments(
            SqlConnection conn,
            string documentRole = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? typeId = null,
            int? lin = null)
        {
            return conn.CreateStoreCommand(
                    @"SELECT
                        p.LotId,
                        p.Lin,
                        ii.Name,
                        nv.Name as Type,
                        ii.Number,
                        ii.FromDate,
                        ii.Date,
                        ii.ToDate,
                        ii.Publisher,
                        ii.Valid
                    FROM 
                    GvaViewInventoryItems ii
                    INNER JOIN GvaViewPersons p ON ii.LotId = p.LotId
                    LEFT JOIN NomValues nv ON nv.NomValueId = ii.TypeId
                    WHERE 1=1 {0} {1} {2} {3} {4}
                    ORDER BY ii.FromDate DESC",
                    new DbClause("and ii.FromDate >= {0}", fromDate),
                    new DbClause("and ii.ToDate <= {0}", toDate),
                    new DbClause("and p.Lin = {0}", lin),
                    new DbClause("and nv.NomValueId = {0}", typeId),
                    new DbClause("and ii.Name = {0}", documentRole))
                .Materialize(r =>
                        new PersonReportDocumentDO()
                        {
                            LotId = r.Field<int>("LotId"),
                            Lin = r.Field<int?>("Lin"),
                            Name = r.Field<string>("Name"),
                            Type = r.Field<string>("Type"),
                            Number = r.Field<string>("Number"),
                            FromDate = r.Field<DateTime?>("FromDate") ?? r.Field<DateTime?>("Date"),
                            ToDate = r.Field<DateTime?>("ToDate"),
                            Valid = r.Field<bool?>("Valid"),
                            Publisher = r.Field<string>("Publisher")
                        })
                .ToList();
        }

        public List<PersonReportLicenceDO> GetLicences(
            SqlConnection conn,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null)
        {
            return conn.CreateStoreCommand(
                        @"SELECT
                             p.LotId,
                             p.Lin,
                             p.Uin AS uin,
                             p.Names,
                             lt.Name AS LicenceTypeName,
                             l.PublisherCode + ' ' + l.LicenceTypeCaCode + ' ' + RIGHT('00000'+CAST(l.LicenceNumber AS NVARCHAR(5)),5) AS LicenceCode,
                             le.DateValidFrom,
                             le.DateValidTo,
                             le.FirstDocDateValidFrom AS FirstIssueDate, 
                             la.Name AS LicenceAction,
                             le.StampNumber
                         FROM 
                         GvaViewPersonLicenceEditions le
                         INNER JOIN GvaViewPersonLicences l ON le.LotId = l.LotId and le.LicencePartIndex = l.PartIndex
                         INNER JOIN GvaViewPersons p ON l.LotId = p.LotId
                         INNER JOIN NomValues lt ON lt.NomValueId = l.LicenceTypeId
                         INNER JOIN NomValues la ON la.NomValueId = le.LicenceActionId
                         WHERE 1=1 {0} {1} {2} {3} {4}
                         ORDER BY le.DateValidFrom DESC",
                        new DbClause("and le.DateValidFrom >= {0}", fromDate),
                        new DbClause("and le.DateValidTo <= {0}", toDate),
                        new DbClause("and p.Lin = {0}", lin),
                        new DbClause("and lt.NomValueId = {0}", licenceTypeId),
                        new DbClause("and la.NomValueId = {0}", licenceActionId))
                    .Materialize(r =>
                            new PersonReportLicenceDO()
                            {
                                LotId = r.Field<int>("lotId"),
                                Lin = r.Field<int?>("lin"),
                                Uin = r.Field<string>("uin"),
                                Names = r.Field<string>("names"),
                                LicenceTypeName = r.Field<string>("licenceTypeName"),
                                LicenceCode = r.Field<string>("licenceCode"),
                                DateValidFrom = r.Field<DateTime?>("dateValidFrom"),
                                DateValidTo = r.Field<DateTime?>("dateValidTo"),
                                FirstIssueDate = r.Field<DateTime?>("firstIssueDate"),
                                LicenceAction = r.Field<string>("licenceAction"),
                                StampNumber = r.Field<string>("stampNumber")
                            })
                    .ToList();
        }

        public List<PersonReportRatingDO> GetRatings(
            SqlConnection conn,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null)
        {
            return conn.CreateStoreCommand(@"
                         SELECT 
                            p.Lin,
                            r.LotId,
                            lastEdition.RatingPartIndex,
                            re2.DocDateValidFrom AS firstIssueDate,
                            re.RatingSubClasses,
                            re.Limitations,
                            re.DocDateValidFrom,
                            re.DocDateValidTo,
                            r.RatingTypes,
                            r.Sector,
                            rc.Code as RatingClass,
                            a.Code as AuthorizationCode,
                            ct.Code as AircraftTypeCategory,
                            atg.Code as AircraftTypeGroup,
                            li.Code as LocationIndicator,
                            rl.Code as RatingLevel
                        FROM  GvaViewPersonRatings r
                        INNER JOIN GvaViewPersons p ON r.LotId = p.LotId
                        INNER JOIN (select 
                            r.LotId,
                            max(re.PartIndex) as edition_part_index,
                            re.RatingPartIndex
                            FROM  GvaViewPersonRatings r
                            INNER JOIn GvaViewPersonRatingEditions re on r.LotId = re.LotId and r.PartIndex = re.RatingPartIndex
                            group by re.RatingPartIndex, r.LotId) lastEdition on lastEdition.LotId = r.LotId and lastEdition.RatingPartIndex = r.PartIndex
                        INNER JOIN GvaViewPersonRatingEditions re on lastEdition.LotId = re.LotId and re.PartIndex = lastEdition.edition_part_index
                        INNER JOIN (select 
                            r.LotId,
                            min(re.PartIndex) AS edition_part_index,
                            re.RatingPartIndex
                            FROM  GvaViewPersonRatings r
                            INNER JOIn GvaViewPersonRatingEditions re on r.LotId = re.LotId and r.PartIndex = re.RatingPartIndex
                            group by re.RatingPartIndex, r.LotId) firstEdition on firstEdition.LotId = lastEdition.LotId and firstEdition.RatingPartIndex = lastEdition.RatingPartIndex
                        INNER JOIN GvaViewPersonRatingEditions re2 on firstEdition.LotId = re2.LotId and re2.PartIndex = firstEdition.edition_part_index
                        LEFT JOIN NomValues rc ON rc.NomValueId = r.RatingClassId
                        LEFT JOIN NomValues a ON a.NomValueId = r.AuthorizationId
                        LEFT JOIN NomValues ct ON ct.NomValueId = r.AircraftTypeCategoryId
                        LEFT JOIN NomValues atg ON atg.NomValueId = r.AircraftTypeGroupId
                        LEFT JOIN NomValues li ON li.NomValueId = r.LocationIndicatorId
                        LEFT JOIN NomValues rl ON rl.NomValueId = r.RatingLevelId
                        WHERE 1=1 {0} {1} {2} {3} {4}
                        ORDER BY re.DocDateValidFrom DESC",
                        new DbClause("and re.DocDateValidFrom >= {0}", fromDate),
                        new DbClause("and re.DocDateValidTo <= {0}", toDate),
                        new DbClause("and p.Lin = {0}", lin),
                        new DbClause("and r.RatingClassId = {0}", ratingClassId),
                        new DbClause("and r.AuthorizationId = {0}", authorizationId),
                        new DbClause("and r.AircraftTypeGroupId = {0}", aircraftTypeCategoryId))
                    .Materialize(r =>
                            new PersonReportRatingDO()
                            {
                                Lin = r.Field<int?>("lin"),
                                LotId = r.Field<int>("lotId"),
                                RatingSubClasses = r.Field<string>("RatingSubClasses"),
                                Limitations = r.Field<string>("Limitations"),
                                FirstIssueDate = r.Field<DateTime?>("FirstIssueDate"),
                                DateValidFrom = r.Field<DateTime?>("DocDateValidFrom"),
                                DateValidTo = r.Field<DateTime?>("DocDateValidTo"),
                                RatingTypes = r.Field<string>("RatingTypes"),
                                Sector = r.Field<string>("Sector"),
                                RatingClass = r.Field<string>("RatingClass"),
                                AuthorizationCode = r.Field<string>("AuthorizationCode"),
                                AircraftTypeCategory = r.Field<string>("AircraftTypeCategory"),
                                AircraftTypeGroup = r.Field<string>("AircraftTypeGroup"),
                                LocationIndicator = r.Field<string>("LocationIndicator"),
                                RatingLevel = r.Field<string>("RatingLevel")
                            })
                    .ToList();
        }

        public XLWorkbook GetDocumentsWorkbook(
            SqlConnection conn,
            string documentRole = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? typeId = null,
            int? lin = null)
        {
            List <PersonReportDocumentDO> documents = this.GetDocuments(
                conn: conn,
                lin: lin,
                documentRole: documentRole,
                fromDate: fromDate,
                toDate: toDate,
                typeId: typeId);

            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Documents");
            
            //headers
            int rowIndex = 1;

            var firstRowStyle = ws.Row(1).Style;
            firstRowStyle.Font.Bold = true;
            firstRowStyle.Font.FontColor = XLColor.DarkSlateGray;
            firstRowStyle.Fill.BackgroundColor = XLColor.Lavender;

            ws.Cell(rowIndex, "A").Value = "Лин";
            ws.Cell(rowIndex, "B").Value = "Документ (роля)";
            ws.Cell(rowIndex, "C").Value = "Вид";
            ws.Cell(rowIndex, "D").Value = "№ на документа";
            ws.Cell(rowIndex, "E").Value = "Издател";
            ws.Cell(rowIndex, "F").Value = "Валиден";
            ws.Cell(rowIndex, "G").Value = "От дата";
            ws.Cell(rowIndex, "H").Value = "До дата";

            rowIndex++;
            foreach (var document in documents)
            {
                ws.Cell(rowIndex, "A").Value = document.Lin;
                ws.Cell(rowIndex, "B").Value = document.Name;
                ws.Cell(rowIndex, "C").Value = document.Type;
                ws.Cell(rowIndex, "D").Value = document.Number;
                ws.Cell(rowIndex, "E").Value = document.Publisher;
                ws.Cell(rowIndex, "F").Value = document.Valid.HasValue? (document.Valid.Value ? "Да" : "Не") : null;
                ws.Cell(rowIndex, "G").Value = document.FromDate.HasValue? document.FromDate.Value.ToString("dd.MM.yyyy") : null;
                ws.Cell(rowIndex, "H").Value = document.ToDate.HasValue ? document.ToDate.Value.ToString("dd.MM.yyyy") : null;
                rowIndex++;
            }

            ws.Columns().AdjustToContents();
            ws.Column("C").Width = 45;
            ws.Column("E").Width = 45;

            return workbook;
        }

        public XLWorkbook GetLicencesWorkbook(
                    SqlConnection conn,
                    DateTime? fromDate = null,
                    DateTime? toDate = null,
                    int? licenceActionId = null,
                    int? licenceTypeId = null,
                    int? lin = null)
        {
            List<PersonReportLicenceDO> licences = this.GetLicences(
                    conn: conn,
                    fromDate: fromDate,
                    toDate: toDate,
                    licenceActionId: licenceActionId,
                    licenceTypeId: licenceTypeId,
                    lin: lin);

            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Licences");

            //headers
            int rowIndex = 1;

            var firstRowStyle = ws.Row(1).Style;
            firstRowStyle.Font.Bold = true;
            firstRowStyle.Font.FontColor = XLColor.DarkSlateGray;
            firstRowStyle.Fill.BackgroundColor = XLColor.Lavender;

            ws.Cell(rowIndex, "A").Value = "Лин";
            ws.Cell(rowIndex, "B").Value = "ЕГН";
            ws.Cell(rowIndex, "C").Value = "Име";
            ws.Cell(rowIndex, "D").Value = "№";
            ws.Cell(rowIndex, "E").Value = "Тип лиценз";
            ws.Cell(rowIndex, "F").Value = "Дата на първо";
            ws.Cell(rowIndex, "G").Value = "От дата";
            ws.Cell(rowIndex, "H").Value = "До дата";
            ws.Cell(rowIndex, "I").Value = "Основание";
            ws.Cell(rowIndex, "J").Value = "№ на печат";

            rowIndex++;
            foreach (var licence in licences)
            {
                ws.Cell(rowIndex, "A").Value = licence.Lin;
                ws.Cell(rowIndex, "B").Value = licence.Uin;
                ws.Cell(rowIndex, "C").Value = licence.Names;
                ws.Cell(rowIndex, "D").Value = licence.LicenceCode;
                ws.Cell(rowIndex, "E").Value = licence.LicenceTypeName;
                ws.Cell(rowIndex, "F").Value = licence.FirstIssueDate.HasValue ? licence.FirstIssueDate.Value.ToString("dd.MM.yyyy") : null;
                ws.Cell(rowIndex, "G").Value = licence.DateValidFrom.HasValue ? licence.DateValidFrom.Value.ToString("dd.MM.yyyy") : null;
                ws.Cell(rowIndex, "H").Value = licence.DateValidTo.HasValue ? licence.DateValidTo.Value.ToString("dd.MM.yyyy") : null;
                ws.Cell(rowIndex, "I").Value = licence.LicenceAction;
                ws.Cell(rowIndex, "J").Value = licence.StampNumber;

                rowIndex++;
            }
            ws.Columns().AdjustToContents();
            ws.Column("I").Width = 45;

            return workbook;
        }
    }
}
