using Gva.Api.Models;
using System;

namespace Gva.Api.ModelsDO
{
    public class ApplicationListDO
    {
        public int ApplicationId { get; set; }
        public int? DocId { get; set; }

        public int? AppPartId { get; set; }
        public int? AppPartIndex { get; set; }
        public DateTime? AppPartRequestDate { get; set; }
        public string AppPartDocumentNumber { get; set; }
        public string AppPartApplicationTypeName { get; set; }
        public string AppPartStatusName { get; set; }

        public int? PersonId { get; set; }
        public string PersonLin { get; set; }
        public string PersonNames { get; set; }

        public string Description
        {
            get
            {
                if (PersonId.HasValue)
                {
                    return string.Format("{0} - {1}", this.PersonLin, this.PersonNames);
                }

                return "";
            }
        }
    }
}
