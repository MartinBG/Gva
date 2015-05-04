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

namespace Gva.Api.Controllers.Reports
{

    [Authorize]
    [RoutePrefix("api/reports/persons/excelExport")]
    public class PersonsReportsExportExcelController : ExcelController
    {
        private IPersonsReportRepository personsReportRepository;

        public PersonsReportsExportExcelController(IPersonsReportRepository personsReportRepository)
        {
            this.personsReportRepository = personsReportRepository;
        }

        [HttpGet]
        [Route("documents")]
        public HttpResponseMessage GetDocumentsReportExcelExport(
            string documentRole = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? typeId = null,
            int? lin = null)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var workbook = this.personsReportRepository.GetDocumentsWorkbook(
                    conn: conn,
                    documentRole: documentRole,
                    fromDate: fromDate,
                    toDate: toDate,
                    typeId: typeId,
                    lin: lin);

                return this.GetExcelFile(workbook, "documents");
            }
        }
    }
}
