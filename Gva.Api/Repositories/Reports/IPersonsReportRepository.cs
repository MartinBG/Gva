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
    public interface IPersonsReportRepository
    {
        Tuple<int, List<PersonReportDocumentDO>> GetDocuments(
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
            string publisher = null,
            int? medClassId = null,
            int offset = 0,
            int limit = 10);

        Tuple<int, List<PersonReportLicenceDO>> GetLicences(
            SqlConnection conn,
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null,
            int? limitationId = null,
            int offset = 0,
            int limit = 10);

        Tuple<int, List<PersonReportRatingDO>> GetRatings(
            SqlConnection conn,
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null,
            int? limitationId = null,
            int offset = 0,
            int limit = 10);
    }
}
