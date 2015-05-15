using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using ClosedXML.Excel;
using Gva.Api.Models;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Persons.Reports;

namespace Gva.Api.Repositories.Reports
{
    public interface IPersonsReportsExportExcelRepository
    {
        XLWorkbook GetDocumentsWorkbook(
           SqlConnection conn,
           string documentRole = null,
           DateTime? fromDate = null,
           DateTime? toDate = null,
           int? typeId = null,
           int? lin = null,
           int? limitationId = null);

        XLWorkbook GetLicencesWorkbook(
            SqlConnection conn,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? licenceActionId = null,
            int? licenceTypeId = null,
            int? lin = null,
            int? limitationId = null);

        XLWorkbook GetRatingsWorkbook(
            SqlConnection conn,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null,
            int? limitationId = null);
    }
}
