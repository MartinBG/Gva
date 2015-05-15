using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Http;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.Reports;

namespace Gva.Api.Controllers.Reports
{
    [RoutePrefix("api/reports/persons")]
    [Authorize]
    public class PersonsReportsController : ApiController
    {
        private IPersonsReportRepository personsReportRepository;

        public PersonsReportsController(IPersonsReportRepository personsReportRepository)
        {
            this.personsReportRepository = personsReportRepository;
        }

        [Route(@"documents")]
        public IHttpActionResult GetDocuments(
            string documentRole = null,
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? typeId = null,
            int? lin = null,
            int? limitationId = null,
            int offset = 0, 
            int limit = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var result = this.personsReportRepository.GetDocuments(
                    conn: conn,
                    documentRole: documentRole,
                    fromDate: fromDate,
                    toDate: toDate,
                    typeId: typeId,
                    lin: lin,
                    limitationId: limitationId);

                return Ok(new
                {
                    documentsCount = result.Count(),
                    documents = result.Skip(offset).Take(limit).ToList()
                });
            }
        }

        [Route(@"licenceCerts")]
        public IHttpActionResult GetLicences(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null,
            int? limitationId = null,
            int offset = 0,
            int limit = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var licences = this.personsReportRepository.GetLicences(
                    conn: conn,
                    fromDate: fromDate,
                    toDate: toDate,
                    licenceActionId: licenceActionId,
                    licenceTypeId: licenceTypeId,
                    limitationId: limitationId,
                    lin: lin);

                return Ok(new
                {
                    licencesCount = licences.Count(),
                    licences = licences.Skip(offset).Take(limit).ToList()
                });
            }
        }

        [Route(@"ratings")]
        public IHttpActionResult GetRatings(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null,
            int? limitationId = null,
            int offset = 0,
            int limit = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var ratings = this.personsReportRepository.GetRatings(
                    conn: conn,
                    fromDate: fromDate,
                    toDate: toDate,
                    ratingClassId: ratingClassId,
                    authorizationId: authorizationId,
                    aircraftTypeCategoryId: aircraftTypeCategoryId,
                    lin: lin,
                    limitationId: limitationId);

                return Ok(new
                {
                    ratingsCount = ratings.Count(),
                    ratings = ratings.Skip(offset).Take(limit).ToList()
                });
            }
        }
    }
}
