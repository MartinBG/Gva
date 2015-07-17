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
                var result = this.personsReportRepository.GetDocuments(
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
                    offset: offset,
                    limit: limit);

                return Ok(new
                {
                    documentsCount = result.Item1,
                    documents = result.Item2
                });
            }
        }

        [Route(@"licenceCerts")]
        public IHttpActionResult GetLicences(
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
                var result = this.personsReportRepository.GetLicences(
                    conn: conn,
                    fromDatePeriodFrom: fromDatePeriodFrom,
                    fromDatePeriodTo: fromDatePeriodTo,
                    toDatePeriodFrom: toDatePeriodFrom,
                    toDatePeriodTo: toDatePeriodTo,
                    licenceActionId: licenceActionId,
                    licenceTypeId: licenceTypeId,
                    limitationId: limitationId,
                    lin: lin,
                    sortBy: sortBy,
                    offset: offset,
                    limit: limit);

                return Ok(new
                {
                    licencesCount = result.Item1,
                    licences = result.Item2
                });
            }
        }

        [Route(@"ratings")]
        public IHttpActionResult GetRatings(
            DateTime? fromDatePeriodFrom = null,
            DateTime? fromDatePeriodTo = null,
            DateTime? toDatePeriodFrom = null,
            DateTime? toDatePeriodTo = null,
            int? ratingClassId = null,
            int? authorizationId = null,
            int? aircraftTypeCategoryId = null,
            int? lin = null,
            int? limitationId = null,
            int? ratingTypeId = null,
            string sortBy = null,
            int? showAllPerPersonId = null,
            int offset = 0,
            int limit = 10)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var result = this.personsReportRepository.GetRatings(
                    conn: conn,
                    fromDatePeriodFrom: fromDatePeriodFrom,
                    fromDatePeriodTo: fromDatePeriodTo,
                    toDatePeriodFrom: toDatePeriodFrom,
                    toDatePeriodTo: toDatePeriodTo,
                    ratingClassId: ratingClassId,
                    authorizationId: authorizationId,
                    aircraftTypeCategoryId: aircraftTypeCategoryId,
                    lin: lin,
                    limitationId: limitationId,
                    ratingTypeId: ratingTypeId,
                    showAllPerPersonId: showAllPerPersonId,
                    sortBy: sortBy,
                    offset: offset,
                    limit: limit);

                return Ok(new
                {
                    ratingsCount = result.Item1,
                    ratings = result.Item2
                });
            }
        }
    }
}
