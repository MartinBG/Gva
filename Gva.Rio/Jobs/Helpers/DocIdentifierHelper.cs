using Docs.Api.Models;
using Gva.Portal.RioObjects;
using R_0009_000016;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gva.Rio.Jobs.Helpers
{
    public class DocIdentifierHelper
    {
        public string ElectronicServiceFileTypeUri { get; set; }

        public DocFileType DocFileType { get; set; }
        public DocType DocType { get; set; }

        public IHeaderFooterDocumentsRioApplication ServiceHeader { get; set; }
        public ElectronicServiceApplicant DocServiceApplicant { get; set; }
        public List<Correspondent> DocCorrespondents { get; set; }
    }
}