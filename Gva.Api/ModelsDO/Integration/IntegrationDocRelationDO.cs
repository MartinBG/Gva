using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;
using Docs.Api.Models;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.ModelsDO.Integration
{
    public class IntegrationDocRelationDO
    {
        public IntegrationDocRelationDO()
        {
        }

        public IntegrationDocRelationDO(DocRelation d)
        {
            this.DocRelationId = d.DocRelationId;
            this.DocId = d.DocId;

            if (d.Doc != null)
            {
                this.DocRegUri = d.Doc.RegUri;

                this.DocDocTypeId = d.Doc.DocType != null ? d.Doc.DocType.DocTypeId : (int?)null;
                this.DocDocTypeName = d.Doc.DocType != null ? d.Doc.DocType.Name : string.Empty;
            }
        }

        public int DocRelationId { get; set; }

        public int? DocId { get; set; }

        public string DocRegUri { get; set; }

        public int? DocDocTypeId { get; set; }

        public string DocDocTypeName { get; set; }

        public NomValue ApplicationType { get; set; }

        public List<GvaCaseType> CaseTypes { get; set; }

        public String Set { get; set; }

        public PersonDataDO PersonData { get; set; }

        public AircraftDO AircraftData { get; set; }

    }
}
