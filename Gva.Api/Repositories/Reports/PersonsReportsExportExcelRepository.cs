using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using ClosedXML.Excel;
using Gva.Api.ModelsDO.Persons.Reports;

namespace Gva.Api.Repositories.Reports
{
    public class PersonsReportsExportExcelRepository : IPersonsReportsExportExcelRepository
    {
        private IPersonsReportRepository personReportRepository;

        public PersonsReportsExportExcelRepository(IPersonsReportRepository personReportRepository)
        {
            this.personReportRepository = personReportRepository;
        }

        public XLWorkbook GetDocumentsWorkbook(
            SqlConnection conn,
            string documentRole = null,
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? typeId = null,
            int? lin = null,
            int? limitationId = null,
            string docNumber = null,
            string publisher = null)
        {
            List<PersonReportDocumentDO> documents = this.personReportRepository.GetDocuments(
                conn: conn,
                lin: lin,
                documentRole: documentRole,
                fromDatePeriodFrom: fromDatePeriodFrom,
                fromDatePeriodTo: fromDatePeriodTo,
                toDatePeriodFrom: toDatePeriodFrom,
                toDatePeriodTo: toDatePeriodTo,
                typeId: typeId,
                limitationId: limitationId,
                docNumber: docNumber,
                publisher: publisher);

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
            ws.Cell(rowIndex, "C").Value = "Тип документ";
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
                ws.Cell(rowIndex, "F").Value = document.Valid.HasValue ? (document.Valid.Value ? "Да" : "Не") : null;
                ws.Cell(rowIndex, "G").Value = document.FromDate.HasValue ? document.FromDate.Value.ToString("dd.MM.yyyy") : null;
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
                    DateTime? fromDatePeriodFrom = null,
                    DateTime? fromDatePeriodTo = null,
                    DateTime? toDatePeriodFrom = null,
                    DateTime? toDatePeriodTo = null,
                    int? licenceActionId = null,
                    int? licenceTypeId = null,
                    int? lin = null,
                    int? limitationId = null)
        {
            List<PersonReportLicenceDO> licences = this.personReportRepository.GetLicences(
                    conn: conn,
                    fromDatePeriodFrom: fromDatePeriodFrom,
                    fromDatePeriodTo: fromDatePeriodTo,
                    toDatePeriodFrom: toDatePeriodFrom,
                    toDatePeriodTo: toDatePeriodTo,
                    licenceActionId: licenceActionId,
                    licenceTypeId: licenceTypeId,
                    lin: lin,
                    limitationId: limitationId);

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
            ws.Cell(rowIndex, "K").Value = "Ограничения";
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
                ws.Cell(rowIndex, "K").Value = licence.Limitations;
                rowIndex++;
            }
            ws.Columns().AdjustToContents();
            ws.Column("I").Width = 45;

            return workbook;
        }

        public XLWorkbook GetRatingsWorkbook(
            SqlConnection conn,
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null,
            int? limitationId = null)
        {
            List<PersonReportRatingDO> ratings = this.personReportRepository.GetRatings(
                        conn: conn,
                        fromDatePeriodFrom: fromDatePeriodFrom,
                        fromDatePeriodTo: fromDatePeriodTo,
                        toDatePeriodFrom: toDatePeriodFrom,
                        toDatePeriodTo: toDatePeriodTo,
                        ratingClassId: ratingClassId,
                        authorizationId: authorizationId,
                        lin: lin,
                        limitationId: limitationId);

            var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add("Ratings");

            //headers
            int rowIndex = 1;

            var firstRowStyle = ws.Row(1).Style;
            firstRowStyle.Font.Bold = true;
            firstRowStyle.Font.FontColor = XLColor.DarkSlateGray;
            firstRowStyle.Fill.BackgroundColor = XLColor.Lavender;

            ws.Cell(rowIndex, "A").Value = "Лин";
            ws.Cell(rowIndex, "B").Value = "Издаден";
            ws.Cell(rowIndex, "C").Value = "Валиден до";
            ws.Cell(rowIndex, "D").Value = "Първоначално издаване";
            ws.Cell(rowIndex, "E").Value = "Тип ВС";
            ws.Cell(rowIndex, "F").Value = "Степен(раб. място)";
            ws.Cell(rowIndex, "G").Value = "Клас";
            ws.Cell(rowIndex, "H").Value = "Подклас";
            ws.Cell(rowIndex, "I").Value = "Категория";
            ws.Cell(rowIndex, "J").Value = "Разрешение";
            ws.Cell(rowIndex, "K").Value = "Ограничения";

            rowIndex++;
            foreach (var rating in ratings)
            {
                ws.Cell(rowIndex, "A").Value = rating.Lin;
                ws.Cell(rowIndex, "B").Value = rating.DateValidFrom.HasValue ? rating.DateValidFrom.Value.ToString("dd.MM.yyyy") : null;
                ws.Cell(rowIndex, "C").Value = rating.DateValidTo.HasValue ? rating.DateValidTo.Value.ToString("dd.MM.yyyy") : null;
                ws.Cell(rowIndex, "D").Value = rating.FirstIssueDate.HasValue ? rating.FirstIssueDate.Value.ToString("dd.MM.yyyy") : null;
                ws.Cell(rowIndex, "E").Value = rating.AircraftTypeCategory;
                ws.Cell(rowIndex, "F").Value = string.Format("{0} {1}", rating.LocationIndicator, rating.Sector);
                ws.Cell(rowIndex, "G").Value = rating.RatingClass;
                ws.Cell(rowIndex, "H").Value = rating.RatingSubClasses;
                ws.Cell(rowIndex, "I").Value = rating.AircraftTypeCategory;
                ws.Cell(rowIndex, "J").Value = rating.AuthorizationCode;
                ws.Cell(rowIndex, "K").Value = rating.Limitations;

                rowIndex++;
            }
            ws.Columns().AdjustToContents();

            return workbook;
        }
    }
}
