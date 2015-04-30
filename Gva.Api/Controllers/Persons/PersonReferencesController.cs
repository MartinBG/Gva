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

namespace Gva.Api.Controllers.Persons
{
    [RoutePrefix("api/persons/references")]
    [Authorize]
    public class PersonReferencesController : ApiController
    {
        private IInventoryRepository inventoryRepository;

        public PersonReferencesController(
            IInventoryRepository inventoryRepository)
        {
            this.inventoryRepository = inventoryRepository;
        }

        [Route(@"documents")]
        public IHttpActionResult GetDocuments(string documentPart = null, DateTime? fromDate = null, DateTime? toDate = null, int? typeId = null)
        {
            var documents = this.inventoryRepository.GetInventoryItems(setAlias: "Person", documentPart: documentPart, fromDate: fromDate, toDate: toDate, typeId: typeId);

            return Ok(documents.Select(i => new PersonReferenceDocumentDO(i)).ToList());
        }

        [Route(@"licenceCerts")]
        public IHttpActionResult GetLicences(
            DateTime? fromDate = null,
            DateTime? toDate = null,
            string lin = null,
            int? licenceTypeId = null,
            int? licenceActionId = null)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                string whereClause = "";
                if (fromDate.HasValue || toDate.HasValue || !string.IsNullOrEmpty(lin) || licenceTypeId.HasValue)
                { 
                    List<string>conditions = new List<string>();
                    if(fromDate.HasValue)
                    {
                        conditions.Add(string.Format("CONVERT(nvarchar(30), le.DateValidFrom, 104) >= '{0}'", fromDate.Value.ToString("dd.MM.yyyy")));
                    }
                    if (toDate.HasValue)
                    {
                        conditions.Add(string.Format("CONVERT(nvarchar(30), le.DateValidTo, 104) <= '{0}'", toDate.Value.ToString("dd.MM.yyyy")));
                    }
                    if (!string.IsNullOrEmpty(lin))
                    {
                        conditions.Add(string.Format("lin = {0}", lin));
                    }
                    if (licenceTypeId.HasValue)
                    {
                        conditions.Add(string.Format("lt.NomValueId = {0}", licenceTypeId.Value));
                    }
                    if (licenceActionId.HasValue)
                    {
                        conditions.Add(string.Format("la.NomValueId = {0}", licenceActionId.Value));
                    }

                    whereClause = "WHERE " + string.Join(" and ", conditions.ToArray());
                }

                var licences =
                    conn.CreateStoreCommand(@"
                         SELECT
                             p.Lin,
                             COALESCE(p.Uin, '') AS uin,
                             p.Names,
                             lt.Name AS LicenceTypeName,
                             l.PublisherCode + ' ' + l.LicenceTypeCaCode + ' ' + RIGHT('00000'+CAST(l.LicenceNumber AS NVARCHAR(5)),5) AS LicenceCode,
                             CONVERT(nvarchar(30), le.DateValidFrom, 104) as DateValidFrom,
                             COALESCE(CONVERT(nvarchar(30), le.DateValidTo, 104), '') AS DateValidTo,
                             CONVERT(nvarchar(30), le.FirstDocDateValidFrom, 104) AS FirstIssueDate, 
                             la.Name AS LicenceAction,
                             COALESCE(le.StampNumber, '') AS StampNumber
                         FROM 
                         GvaViewPersonLicenceEditions le
                         INNER JOIN GvaViewPersonLicences l ON le.LotId = l.LotId and le.LicencePartIndex = l.PartIndex
                         INNER JOIN GvaViewPersons p ON l.LotId = p.LotId
                         INNER JOIN NomValues lt ON lt.NomValueId = l.LicenceTypeId
                         INNER JOIN NomValues la ON la.NomValueId = le.LicenceActionId
                         {0}
                         ORDER BY le.DateValidFrom desc",
                        new DbClause(whereClause))
                    .Materialize(r =>
                            new PersonReferenceLicenceDO()
                            {
                                Lin = r.Field<int?>("lin"),
                                Uin = r.Field<string>("uin"),
                                Names = r.Field<string>("names"),
                                LicenceTypeName = r.Field<string>("licenceTypeName"),
                                LicenceCode = r.Field<string>("licenceCode"),
                                DateValidFrom = !string.IsNullOrEmpty(r.Field<string>("dateValidFrom")) ? DateTime.Parse(r.Field<string>("dateValidFrom")) : (DateTime?)null,
                                DateValidTo = !string.IsNullOrEmpty(r.Field<string>("dateValidTo")) ? DateTime.Parse(r.Field<string>("dateValidTo")) : (DateTime?)null,
                                FirstIssueDate = !string.IsNullOrEmpty(r.Field<string>("firstIssueDate")) ? DateTime.Parse(r.Field<string>("firstIssueDate")) : (DateTime?)null,
                                LicenceAction = r.Field<string>("licenceAction"),
                                StampNumber = r.Field<string>("stampNumber")
                            })
                    .ToList();

                return Ok(licences);
            }
        }
    }
}
