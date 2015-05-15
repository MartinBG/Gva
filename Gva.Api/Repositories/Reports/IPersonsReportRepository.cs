using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;
using ClosedXML.Excel;
using Gva.Api.Models;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Persons.Reports;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IPersonsReportRepository
    {
        List<PersonReportDocumentDO> GetDocuments(
            SqlConnection conn,
            string documentRole = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? typeId = null,
            int? lin = null);

        List<PersonReportLicenceDO> GetLicences(
            SqlConnection conn,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null,
            int? limitationId = null);

        List<PersonReportRatingDO> GetRatings(
            SqlConnection conn,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null,
            int? limitationId = null);

        XLWorkbook GetDocumentsWorkbook(
           SqlConnection conn,
           string documentRole = null,
           DateTime? fromDate = null,
           DateTime? toDate = null,
           int? typeId = null,
           int? lin = null);

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
