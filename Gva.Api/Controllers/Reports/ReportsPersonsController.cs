using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Gva.Api.CommonUtils;
using System.Threading.Tasks;
using System.Web.Http;
using Gva.Api.ModelsDO.Persons.Reports;
using Gva.Api.Repositories.InventoryRepository;
using Newtonsoft.Json.Linq;

namespace Gva.Api.Controllers.Reports
{
    [RoutePrefix("api/reports/persons")]
    [Authorize]
    public class ReportsPersonsController : ApiController
    {
        private IInventoryRepository inventoryRepository;

        public ReportsPersonsController(
            IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        [Route(@"documents")]
        public IHttpActionResult GetDocuments(string documentPart = null, DateTime? fromDate = null, DateTime? toDate = null, int? typeId = null)
        {
            var documents = this.inventoryRepository.GetInventoryItems(setAlias: "Person", documentPart: documentPart, fromDate: fromDate, toDate: toDate, typeId: typeId);

            return Ok(documents.Select(i => new PersonReportDocumentDO(i)).ToList());
        }

        [Route(@"licenceCerts")]
        public IHttpActionResult GetLicences(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            int? lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                var licences =
                    conn.CreateStoreCommand(@"
                         SELECT
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
                         ORDER BY le.DateValidFrom desc",
                        new DbClause("and le.DateValidFrom >= {0}", fromDate),
                        new DbClause("and le.DateValidTo >= {0}", toDate),
                        new DbClause("and p.Lin = {0}", lin),
                        new DbClause("and lt.NomValueId = {0}", licenceTypeId),
                        new DbClause("and la.NomValueId = {0}", licenceActionId))
                    .Materialize(r =>
                            new PersonReportLicenceDO()
                            {
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

                return Ok(licences);
            }
        }
    }
}
