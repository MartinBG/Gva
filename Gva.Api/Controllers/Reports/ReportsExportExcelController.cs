using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Gva.Api.CommonUtils;
using Gva.Api.ModelsDO.Persons.Reports;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.Reports;

namespace Gva.Api.Controllers.Reports
{

    [Authorize]
    [RoutePrefix("api/reports/persons/excelExport")]
    public class PersonsReportsExportExcelController : ExcelController
    {
        private IPersonsReportsExportExcelRepository personsReportsExportExcelRepository;

        public PersonsReportsExportExcelController(IPersonsReportsExportExcelRepository personsReportsExportExcelRepository)
        {
            this.personsReportsExportExcelRepository = personsReportsExportExcelRepository;
        }

        [HttpGet]
        [Route("documents")]
        public HttpResponseMessage ExportExcelDocumentsReport(
            int? roleId = null,
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
            string sortBy = null,
            int offset = 0,
            int limit = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var workbook = this.personsReportsExportExcelRepository.GetDocumentsWorkbook(
                    conn: conn,
                    roleId: roleId,
                    fromDatePeriodFrom: fromDatePeriodFrom,
                    fromDatePeriodTo: fromDatePeriodTo,
                    toDatePeriodFrom: toDatePeriodFrom,
                    toDatePeriodTo: toDatePeriodTo,
                    typeId: typeId,
                    lin: lin,
                    limitationId: limitationId,
                    docNumber: docNumber,
                    publisher: publisher,
                    medClassId: medClassId,
                    sortBy: sortBy,
                    limit: limit,
                    offset: offset);

                return this.GetExcelFile(workbook, "documents");
            }
        }

        [HttpGet]
        [Route("licences")]
        public HttpResponseMessage ExportExcelLicencesReport(
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null,
            int? limitationId = null,
            string sortBy = null,
            int offset = 0,
            int limit = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var workbook = this.personsReportsExportExcelRepository.GetLicencesWorkbook(
                    conn: conn,
                    fromDatePeriodFrom: fromDatePeriodFrom,
                    fromDatePeriodTo: fromDatePeriodTo,
                    toDatePeriodFrom: toDatePeriodFrom,
                    toDatePeriodTo: toDatePeriodTo,
                    licenceActionId: licenceActionId,
                    licenceTypeId: licenceTypeId,
                    lin: lin,
                    limitationId: limitationId,
                    sortBy: sortBy,
                    limit: limit,
                    offset: offset);

                return this.GetExcelFile(workbook, "licences");
            }
        }

        [HttpGet]
        [Route("ratings")]
        public HttpResponseMessage ExportExcelRatingsReport(
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? lin = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? limitationId = null,
            int? ratingTypeId = null,
            string sortBy = null,
            int? showAllPerPersonId = null,
            int offset = 0,
            int limit = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var workbook = this.personsReportsExportExcelRepository.GetRatingsWorkbook(
                    conn: conn,
                    fromDatePeriodFrom: fromDatePeriodFrom,
                    fromDatePeriodTo: fromDatePeriodTo,
                    toDatePeriodFrom: toDatePeriodFrom,
                    toDatePeriodTo: toDatePeriodTo,
                    ratingClassId: ratingClassId,
                    authorizationId: authorizationId,
                    lin: lin,
                    limitationId: limitationId,
                    ratingTypeId: ratingTypeId,
                    sortBy: sortBy,
                    showAllPerPersonId: showAllPerPersonId,
                    limit: limit,
                    offset: offset);

                return this.GetExcelFile(workbook, "ratings");
            }
        }
    }
}
