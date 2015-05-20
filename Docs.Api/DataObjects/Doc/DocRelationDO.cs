using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docs.Api.DataObjects
{
    public class DocRelationDO
    {
        public DocRelationDO()
        {
            this.DocUnitsInCharge = new List<NomDo>();
        }

        public DocRelationDO(DocRelation d)
            : this()
        {
            if (d != null)
            {
                this.DocRelationId = d.DocRelationId;
                this.DocId = d.DocId;
                this.ParentDocId = d.ParentDocId;
                this.RootDocId = d.RootDocId;
                this.Version = d.Version;

                if (d.Doc != null)
                {
                    this.DocRegUri = d.Doc.RegUri;
                    this.DocSubject = d.Doc.DocSubject;
                    this.DocRegDate = d.Doc.RegDate;
                    this.DocDocDirectionName = d.Doc.DocDirection != null ? d.Doc.DocDirection.Name : string.Empty;
                    this.DocStatusId = d.Doc.DocStatusId;
                    this.DocVersion = d.Doc.Version;
                    this.DocReceiptOrder = d.Doc.ReceiptOrder;

                    this.DocDocTypeName = d.Doc.DocType != null ? d.Doc.DocType.Name : string.Empty;
                    this.DocDocTypeId = d.Doc.DocType != null ? d.Doc.DocType.DocTypeId : (int?)null;
                    this.DocDocStatusAlias = d.Doc.DocStatus != null ? d.Doc.DocStatus.Alias : string.Empty;
                    this.DocDocStatusName = d.Doc.DocStatus != null ? d.Doc.GetDocStatusName() : string.Empty;
                    this.DocDocCasePartTypeId = d.Doc.DocCasePartTypeId;
                    this.DocDocCasePartTypeName = d.Doc.DocCasePartType != null ? d.Doc.DocCasePartType.Name : string.Empty;
                    this.DocDocCasePartTypeAlias = d.Doc.DocCasePartType != null ? d.Doc.DocCasePartType.Alias : string.Empty;

                    this.DocEntryTypeAlias = d.Doc.DocEntryType != null ? d.Doc.DocEntryType.Alias : string.Empty;

                    if (d.Doc.DocUnits != null)
                    {
                        foreach (var du in d.Doc.DocUnits)
                        {
                            switch (du.DocUnitRole.Alias)
                            {
                                case "InCharge":
                                    this.DocUnitsInCharge.Add(new NomDo(du));
                                    break;
                            };
                        }
                    }
                }
            }
        }

        public int DocRelationId { get; set; }
        public int? DocId { get; set; }
        public int? ParentDocId { get; set; }
        public int? RootDocId { get; set; }
        public byte[] Version { get; set; }

        public string DocRegUri { get; set; }
        public string DocSubject { get; set; }
        public DateTime? DocRegDate { get; set; }
        public string DocDocDirectionName { get; set; }
        public int? DocDocCasePartTypeId { get; set; }
        public string DocDocCasePartTypeName { get; set; }
        public string DocDocCasePartTypeAlias { get; set; }
        public int DocStatusId { get; set; }
        public string DocDocTypeName { get; set; }
        public int? DocDocTypeId { get; set; }
        public string DocDocStatusName { get; set; }
        public string DocDocStatusAlias { get; set; }
        public byte[] DocVersion { get; set; }
        public int? DocReceiptOrder { get; set; }
        public string DocEntryTypeAlias { get; set; }
        public List<NomDo> DocUnitsInCharge { get; set; }
    }
}
