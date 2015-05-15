using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using Gva.Api.CommonUtils;
using Gva.Api.ModelsDO.Persons.Reports;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.InventoryRepository;
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
            string documentRole = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? typeId = null,
            int? lin = null,
            int? limitationId = null)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var workbook = this.personsReportsExportExcelRepository.GetDocumentsWorkbook(
                    conn: conn,
                    documentRole: documentRole,
                    fromDate: fromDate,
                    toDate: toDate,
                    typeId: typeId,
                    lin: lin,
                    limitationId: limitationId);

                return this.GetExcelFile(workbook, "documents");
            }
        }

        [HttpGet]
        [Route("licences")]
        public HttpResponseMessage ExportExcelLicencesReport(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null,
            int? limitationId = null)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var workbook = this.personsReportsExportExcelRepository.GetLicencesWorkbook(
                    conn: conn,
                    fromDate: fromDate,
                    toDate: toDate,
                    licenceActionId: licenceActionId,
                    licenceTypeId: licenceTypeId,
                    lin: lin,
                    limitationId: limitationId);

                return this.GetExcelFile(workbook, "licences");
            }
        }

        [HttpGet]
        [Route("ratings")]
        public HttpResponseMessage ExportExcelRatingsReport(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? lin = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? limitationId = null)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var workbook = this.personsReportsExportExcelRepository.GetRatingsWorkbook(
                    conn: conn,
                    fromDate: fromDate,
                    toDate: toDate,
                    ratingClassId: ratingClassId,
                    authorizationId: authorizationId,
                    lin: lin,
                    limitationId: limitationId);

                return this.GetExcelFile(workbook, "ratings");
            }
        }
    }
}
